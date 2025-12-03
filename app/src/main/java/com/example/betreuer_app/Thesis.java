package com.example.betreuer_app;

public class Thesis {
    // Enums

    public enum Status {
        In_abstimmung, Angemeldet, Abgegeben, Kolloquim_abgehalten
    }

    public enum Rechnungsstatus{
        keine, gestellt, beglichen
    }

    // Attribute

    private int id;
    private String titel;
    private Status status;
    private String fachgebiet;
    private int studentID;
    private int betreuerID;
    private int zweitgutachterID;
    private String exposePfad;
    private Rechnungsstatus rechnungsstatus;



// Konstruktor

public Arbeit(int id, String titel, Status status, String fachgebiet, int studentId,
                    int betreuerId, int zweitgutachterId, String exposePfad,
                    Rechnungsstatus rechnungsstatus) {
        this.id = id;
        this.titel = titel;
        this.status = status;
        this.fachgebiet = fachgebiet;
        this.studentId = studentId;
        this.betreuerId = betreuerId;
        this.zweitgutachterId = zweitgutachterId;
        this.exposePfad = exposePfad;
        this.rechnungsstatus = rechnungsstatus;
    }


// Methoden

public int getId() {
        return id;
    }


public String getTitel() {
        return titel;
    }

public void setTitel(String titel) {
        this.titel = titel;
    }

public Status getStatus() {
        return status;
    }

public void setStatus(Status status) {
        this.status = status;
    }

public String getFachgebiet() {
        return fachgebiet;
    }

public void setFachgebiet(String fachgebiet) {
        this.fachgebiet = fachgebiet;
    }

public int getStudentId() {
        return studentId;
    }

public void setStudentId(int studentId) {
        this.studentId = studentId;
    }

public int getBetreuerId() {
        return betreuerId;
    }

public void setBetreuerId(int betreuerId) {
        this.betreuerId = betreuerId;
    }

public int getZweitgutachterId() {
        return zweitgutachterId;
    }

public void setZweitgutachterId(int zweitgutachterId) {
        this.zweitgutachterId = zweitgutachterId;
    }

public String getExposePfad() {
        return exposePfad;
    }

public void setExposePfad(String exposePfad) {
        this.exposePfad = exposePfad;
    }

public Rechnungsstatus getRechnungsstatus() {
        return rechnungsstatus;
    }

public void setRechnungsstatus(Rechnungsstatus rechnungsstatus) {
        this.rechnungsstatus = rechnungsstatus;
    }



}