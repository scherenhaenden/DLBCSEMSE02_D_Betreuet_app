package com.example.betreuer_app.model;

import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class BillingStatusTest {

    @Test
    public void testConstructorAndGetters() {
        BillingStatus status = new BillingStatus("Keine Rechnung");
        assertEquals("Keine Rechnung", status.getName());
    }

    @Test
    public void testSetName() {
        BillingStatus status = new BillingStatus();
        status.setName("Rechnung gestellt");
        assertEquals("Rechnung gestellt", status.getName());
    }
}
