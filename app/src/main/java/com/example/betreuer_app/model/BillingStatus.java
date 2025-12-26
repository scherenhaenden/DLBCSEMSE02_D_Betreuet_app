package com.example.betreuer_app.model;

import java.util.List;

/**
 * Represents a billing status for a thesis.
 * Billing statuses indicate the payment state of a thesis.
 */
public class BillingStatus extends BaseEntity {
    private String name;
    private List<Thesis> theses;

    /**
     * Default constructor.
     */
    public BillingStatus() {
    }

    /**
     * Constructor for a new billing status.
     * @param name The name of the billing status.
     */
    public BillingStatus(String name) {
        this.name = name;
    }

    /**
     * Returns the name of the billing status.
     * @return The billing status name.
     */
    public String getName() {
        return name;
    }

    /**
     * Sets the name of the billing status.
     * @param name The new billing status name.
     */
    public void setName(String name) {
        this.name = name;
    }

    /**
     * Returns the list of theses with this billing status.
     * @return The list of Thesis objects.
     */
    public List<Thesis> getTheses() {
        return theses;
    }

    /**
     * Sets the list of theses with this billing status.
     * @param theses The new list of Thesis objects.
     */
    public void setTheses(List<Thesis> theses) {
        this.theses = theses;
    }
}
