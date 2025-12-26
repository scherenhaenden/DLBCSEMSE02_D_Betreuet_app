package com.example.betreuer_app.model;

import java.util.List;

/**
 * Represents a status for a thesis.
 * Thesis statuses indicate the progress state of a thesis.
 */
public class ThesisStatus extends BaseEntity {
    private String name;
    private List<Thesis> theses;

    /**
     * Default constructor.
     */
    public ThesisStatus() {
    }

    /**
     * Constructor for a new thesis status.
     * @param name The name of the thesis status.
     */
    public ThesisStatus(String name) {
        this.name = name;
    }

    /**
     * Returns the name of the thesis status.
     * @return The thesis status name.
     */
    public String getName() {
        return name;
    }

    /**
     * Sets the name of the thesis status.
     * @param name The new thesis status name.
     */
    public void setName(String name) {
        this.name = name;
    }

    /**
     * Returns the list of theses with this status.
     * @return The list of Thesis objects.
     */
    public List<Thesis> getTheses() {
        return theses;
    }

    /**
     * Sets the list of theses with this status.
     * @param theses The new list of Thesis objects.
     */
    public void setTheses(List<Thesis> theses) {
        this.theses = theses;
    }
}
