package com.example.betreuer_app.model;

public class LoginResponse {
    private String token;
    private LoggedInUser user;

    public LoginResponse(String token, LoggedInUser user) {
        this.token = token;
        this.user = user;
    }

    public String getToken() {
        return token;
    }

    public void setToken(String token) {
        this.token = token;
    }

    public LoggedInUser getUser() {
        return user;
    }

    public void setUser(LoggedInUser user) {
        this.user = user;
    }
}
