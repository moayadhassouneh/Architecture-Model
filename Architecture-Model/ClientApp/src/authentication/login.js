import React, { Component } from 'react';

export class Login extends Component {
  
    constructor(props) {
        super(props);

        this.LoginAndStoreToken = this.LoginAndStoreToken.bind(this);
    }    


    LoginAndStoreToken()
    {     
        fetch('api/login/login', {           
            method: 'POST', // or 'PUT'
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ "username": "moayad" })
        })
        .then(response => response.json())
            .then(data =>
            {
                if (data.token !== null) {
                    //note: this is not best pracice to store toke, check httponly
                    sessionStorage.setItem("userToken", data.token);
                }
                
            }

        );        
    }    

    render() {
        return (
            <div>
                <h1>Login yalla</h1>

                <p>This is a simple example of Login And Store Token.</p>               

                <button className="btn btn-primary" onClick={this.LoginAndStoreToken}>Login And Store Token</button>
            </div>
        );
    }
}
