package com.example.betreuer_app.model;

import org.junit.Before;
import org.junit.Test;
import static org.junit.Assert.*;

import java.util.UUID;

public class UserTest {

    private User user;
    private UUID userId;

    @Before
    /**
     * Initializes a new user with a random UUID, name, and email.
     */
    public void setUp() {
        userId = UUID.randomUUID();
        user = new User("John", "Doe", "john.doe@example.com", "password");
    }

    @Test
    /**
     * Tests the constructor and getter methods of the user object.
     */
    public void testConstructorAndGetters() {
        assertEquals(userId, user.getId());
        assertEquals("John", user.getFirstName());
        assertEquals("Doe", user.getLastName());
        assertEquals("john.doe@example.com", user.getEmail());
        assertEquals("password", user.getPasswordHash());
    }

    @Test
    public void testSetId() {
        UUID newId = UUID.randomUUID();
        user.setId(newId);
        assertEquals(newId, user.getId());
    }

    @Test
    public void testSetFirstName() {
        user.setFirstName("Jane");
        assertEquals("Jane", user.getFirstName());
    }

    @Test
    public void testSetLastName() {
        user.setLastName("Smith");
        assertEquals("Smith", user.getLastName());
    }

    @Test
    public void testSetEmail() {
        user.setEmail("jane.doe@example.com");
        assertEquals("jane.doe@example.com", user.getEmail());
    }

    @Test
    /**
     * Tests setting the user's role to TUTOR and verifies the role.
     */
    public void testSetRole() {
        user.setRole(new Role("TUTOR"));
        assertEquals("TUTOR", user.getRole().getName());
    }
}
