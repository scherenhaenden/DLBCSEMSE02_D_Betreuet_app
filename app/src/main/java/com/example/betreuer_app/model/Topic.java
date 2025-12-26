package com.example.betreuer_app.model;

import java.util.List;

/**
 * Represents a topic for a thesis.
 */
public class Topic extends BaseEntity {
    private String title;
    private String description;
    private boolean isActive;
    private List<UserTopic> userTopics;

    public Topic() {
    }

    public Topic(String title, String description, boolean isActive) {
        this.title = title;
        this.description = description;
        this.isActive = isActive;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public boolean isActive() {
        return isActive;
    }

    public void setActive(boolean active) {
        isActive = active;
    }

    public List<UserTopic> getUserTopics() {
        return userTopics;
    }

    public void setUserTopics(List<UserTopic> userTopics) {
        this.userTopics = userTopics;
    }
}
