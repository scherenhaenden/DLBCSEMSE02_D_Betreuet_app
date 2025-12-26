package com.example.betreuer_app.model;

import java.util.List;

public class BillingStatus extends BaseEntity {
    private String name;
    private List<Thesis> theses;

    public BillingStatus() {
    }

    public BillingStatus(String name) {
        this.name = name;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public List<Thesis> getTheses() {
        return theses;
    }

    public void setTheses(List<Thesis> theses) {
        this.theses = theses;
    }
}
