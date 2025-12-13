package com.example.betreuer_app.repository;

import com.example.betreuer_app.api.ApiClient;
import com.example.betreuer_app.api.UserApiService;
import com.example.betreuer_app.model.LoginRequest;
import com.example.betreuer_app.model.LoginResponse;

import retrofit2.Call;
import retrofit2.Callback;

public class LoginRepository {
    private UserApiService apiService;

    public LoginRepository() {
        apiService = ApiClient.getUserApiService();
    }

    public void login(String email, String password, Callback<LoginResponse> callback) {
        LoginRequest request = new LoginRequest(email, password);
        Call<LoginResponse> call = apiService.login(request);
        call.enqueue(callback);
    }
}
