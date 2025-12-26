package com.example.betreuer_app.model;

import java.util.Date;
import java.util.UUID;

public abstract class BaseEntity {
    private UUID id = UUID.randomUUID();
    private Date createdAt = new Date();
    private Date updatedAt = new Date();

    public UUID getId() {
        return id;
    }

    public void setId(UUID id) {
        this.id = id;
    }

    public Date getCreatedAt() {
        return createdAt;
    }

    public void setCreatedAt(Date createdAt) {
        this.createdAt = createdAt;
    }

    public Date getUpdatedAt() {
        return updatedAt;
    }

    public void setUpdatedAt(Date updatedAt) {
        this.updatedAt = updatedAt;
    }
}
