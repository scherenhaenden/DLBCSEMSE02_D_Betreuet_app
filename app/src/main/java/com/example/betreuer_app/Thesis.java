package com.example.betreuer_app;

public class Thesis {
    // Enums

    public enum Status {
        IN_ABSTIMMUNG, ANGEMELDET, ABGEGEBEN, KOLLOQUIUM_ABGEHALTEN
    }

    public enum Rechnungsstatus{
        KEINE, GESTELLT, BEGLICHEN
    }

    // Attribute

    private int ID;
    private String titel;
    private Status status;
    private String fachgebiet;
    private int studentID;
    private int betreuerID;
    private int zweitgutachterID;
    private String exposePfad;
    private Rechnungsstatus rechnungsstatus;



// Konstruktor

public Thesis(int ID, String titel, Status status, String fachgebiet, int studentID,
                    int betreuerID, int zweitgutachterID, String exposePfad,
                    Rechnungsstatus rechnungsstatus) {
        this.ID = ID;
        this.titel = titel;
        this.status = status;
        this.fachgebiet = fachgebiet;
        this.studentID = studentID;
        this.betreuerID = betreuerID;
        this.zweitgutachterID = zweitgutachterID;
        this.exposePfad = exposePfad;
        this.rechnungsstatus = rechnungsstatus;
    }


// Methoden

public int getID() {
        return ID;
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

public int getStudentID() {
        return studentID;
    }

public void setStudentID(int studentID) {
        this.studentID = studentID;
    }

public int getBetreuerID() {
        return betreuerID;
    }

public void setBetreuerID(int betreuerID) {
        this.betreuerID = betreuerID;
    }

public int getZweitgutachterID() {
        return zweitgutachterID;
    }

public void setZweitgutachterID(int zweitgutachterID) {
        this.zweitgutachterID = zweitgutachterID;
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