import { Injectable } from '@angular/core';
import { User } from '../dataContracts/userModel';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class UserService {  
    constructor(private router: Router) { }
  
    public getUser(): User{
        const token = localStorage.getItem('token');
        
        if (token) {
            const user = jwtDecode(token);
            return user as User;
        }

        this.router.navigate(['/login']);
        return {} as User; // Return an empty User object if no token is found
    }

    public getTokenTimeValid(): number {
        const token = localStorage.getItem('token');
        if (token) {
            const decodedToken: any = jwtDecode(token);
            return decodedToken.exp ? decodedToken.exp - Math.floor(Date.now() / 1000) : 0;
        }
        return 0; // Return 0 if no token is found
    }
}