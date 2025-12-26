package com.example.betreuer_app.model;

import java.util.UUID;

/**
 * Represents a thesis in the system.
 * Contains information about the thesis, its status, billing, and relationships to users and topics.
 */
public class Thesis extends BaseEntityApiModel {
    private String title;
    private ThesisStatus status;
    private BillingStatusApiModel billingStatus;
    private UUID ownerId;
    private UUID tutorId;
    private UUID secondSupervisorId;
    private UUID topicId;

    /**
     * Default constructor.
     */
    public Thesis() {
    }

    /**
     * Constructor for a new thesis.
     * @param title The title of the thesis.
     * @param status The status of the thesis.
     * @param billingStatus The billing status of the thesis.
     * @param ownerId The ID of the owner (student).
     * @param tutorId The ID of the tutor.
     * @param secondSupervisorId The ID of the second supervisor.
     * @param topicId The ID of the topic.
     */
    public Thesis(String title, ThesisStatus status, BillingStatusApiModel billingStatus, UUID ownerId, UUID tutorId, UUID secondSupervisorId, UUID topicId) {
        this.title = title;
        this.status = status;
        this.billingStatus = billingStatus;
        this.ownerId = ownerId;
        this.tutorId = tutorId;
        this.secondSupervisorId = secondSupervisorId;
        this.topicId = topicId;
    }

    /**
     * Returns the title of the thesis.
     * @return The title.
     */
    public String getTitle() {
        return title;
    }

    /**
     * Sets the title of the thesis.
     * @param title The new title.
     */
    public void setTitle(String title) {
        this.title = title;
    }

    /**
     * Returns the status of the thesis.
     * @return The ThesisStatus object.
     */
    public ThesisStatus getStatus() {
        return status;
    }

    /**
     * Sets the status of the thesis.
     * @param status The new ThesisStatus object.
     */
    public void setStatus(ThesisStatus status) {
        this.status = status;
    }

    /**
     * Returns the billing status of the thesis.
     * @return The BillingStatus object.
     */
    public BillingStatusApiModel getBillingStatus() {
        return billingStatus;
    }

    /**
     * Sets the billing status of the thesis.
     * @param billingStatus The new BillingStatus object.
     */
    public void setBillingStatus(BillingStatusApiModel billingStatus) {
        this.billingStatus = billingStatus;
    }

    /**
     * Returns the owner ID.
     * @return The UUID of the owner.
     */
    public UUID getOwnerId() {
        return ownerId;
    }

    /**
     * Sets the owner ID.
     * @param ownerId The new owner ID.
     */
    public void setOwnerId(UUID ownerId) {
        this.ownerId = ownerId;
    }

    /**
     * Returns the tutor ID.
     * @return The UUID of the tutor.
     */
    public UUID getTutorId() {
        return tutorId;
    }

    /**
     * Sets the tutor ID.
     * @param tutorId The new tutor ID.
     */
    public void setTutorId(UUID tutorId) {
        this.tutorId = tutorId;
    }

    /**
     * Returns the second supervisor ID.
     * @return The UUID of the second supervisor.
     */
    public UUID getSecondSupervisorId() {
        return secondSupervisorId;
    }

    /**
     * Sets the second supervisor ID.
     * @param secondSupervisorId The new second supervisor ID.
     */
    public void setSecondSupervisorId(UUID secondSupervisorId) {
        this.secondSupervisorId = secondSupervisorId;
    }

    /**
     * Returns the topic ID.
     * @return The UUID of the topic.
     */
    public UUID getTopicId() {
        return topicId;
    }

    /**
     * Sets the topic ID.
     * @param topicId The new topic ID.
     */
    public void setTopicId(UUID topicId) {
        this.topicId = topicId;
    }
}
