package com.example.betreuer_app.model;

import java.util.List;
import java.util.UUID;

/**
 * Represents a user in the application.
 */
public class User extends BaseEntity {
    private String firstName;
    private String lastName;
    private String email;
    private String passwordHash;
    private List<UserRole> userRoles;
    private List<UserTopic> userTopics;

    public User() {
    }

    public User(String firstName, String lastName, String email, String passwordHash) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.passwordHash = passwordHash;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getPasswordHash() {
        return passwordHash;
    }

    public void setPasswordHash(String passwordHash) {
        this.passwordHash = passwordHash;
    }

    public List<UserRole> getUserRoles() {
        return userRoles;
    }

    public void setUserRoles(List<UserRole> userRoles) {
        this.userRoles = userRoles;
    }

    public List<UserTopic> getUserTopics() {
        return userTopics;
    }

    public void setUserTopics(List<UserTopic> userTopics) {
        this.userTopics = userTopics;
    }
}
