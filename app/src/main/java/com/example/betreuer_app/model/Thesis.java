package com.example.betreuer_app.model;

import java.util.UUID;

/**
 * Represents a thesis.
 */
public class Thesis extends BaseEntity {
    private String title;
    private ThesisStatus status;
    private BillingStatus billingStatus;
    private UUID ownerId;
    private UUID tutorId;
    private UUID secondSupervisorId;
    private UUID topicId;

    public Thesis() {
    }

    public Thesis(String title, ThesisStatus status, BillingStatus billingStatus, UUID ownerId, UUID tutorId, UUID secondSupervisorId, UUID topicId) {
        this.title = title;
        this.status = status;
        this.billingStatus = billingStatus;
        this.ownerId = ownerId;
        this.tutorId = tutorId;
        this.secondSupervisorId = secondSupervisorId;
        this.topicId = topicId;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public ThesisStatus getStatus() {
        return status;
    }

    public void setStatus(ThesisStatus status) {
        this.status = status;
    }

    public BillingStatus getBillingStatus() {
        return billingStatus;
    }

    public void setBillingStatus(BillingStatus billingStatus) {
        this.billingStatus = billingStatus;
    }

    public UUID getOwnerId() {
        return ownerId;
    }

    public void setOwnerId(UUID ownerId) {
        this.ownerId = ownerId;
    }

    public UUID getTutorId() {
        return tutorId;
    }

    public void setTutorId(UUID tutorId) {
        this.tutorId = tutorId;
    }

    public UUID getSecondSupervisorId() {
        return secondSupervisorId;
    }

    public void setSecondSupervisorId(UUID secondSupervisorId) {
        this.secondSupervisorId = secondSupervisorId;
    }

    public UUID getTopicId() {
        return topicId;
    }

    public void setTopicId(UUID topicId) {
        this.topicId = topicId;
    }
}
