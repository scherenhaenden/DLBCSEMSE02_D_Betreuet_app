using ApiProject.DatabaseAccess.Entities;
using ApiProject.Seed.Services;
using Bogus;
using System.Text.Json;
using Bogus;
using System.Linq.Expressions;
using BCrypt.Net;
using System.Text;

namespace ApiProject.Tests.CreateSeed;

/// <summary>
/// Extended UserDataAccessModel for seeding purposes to include the plain text password.
/// This helps in testing login functionality with known credentials.
/// </summary>
public class UserSeed: UserDataAccessModel
{
    public required string Password { get; set; }
}

[TestFixture]
public class Seeder
{
    /// <summary>
    /// Main method to generate seed data for the application.
    /// This method creates a comprehensive set of data including users, roles, topics, theses, and more,
    /// and saves it to a JSON file for later use in seeding the database.
    /// </summary>
    [Test]
    public void CreateSeed()
    {
        // Get the directory where the executing assembly is located to save the seed.json file.
        string directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        
        ICreateSeedOfObject createSeedOfObject = new CreateSeedOfObject();
        
        // --- 1. Generate Roles ---
        // We define a fixed set of roles: STUDENT, TUTOR, and ADMIN.
        var roleNames = new[] { "STUDENT", "TUTOR", "ADMIN" };
        var rolesToSeed = new List<RoleDataAccessModel>();

        foreach (var roleName in roleNames)
        {
            var role = new Faker<RoleDataAccessModel>()
                .RuleFor(r => r.Id, f => Guid.NewGuid())
                .RuleFor(r => r.Name, roleName) // Set the specific role name
                .RuleFor(r => r.CreatedAt, f => f.Date.Recent())
                .RuleFor(r => r.UpdatedAt, (f, r) => r.CreatedAt)
                .Generate();
    
            rolesToSeed.Add(role);
        }

        // --- 2. Generate Billing Statuses ---
        // Define possible billing statuses for a thesis.
        var billingStatusNames = new[] { "NONE", "ISSUED", "PAID" };
        var billingStatusesToSeed = new List<BillingStatusDataAccessModel>();

        foreach (var statusName in billingStatusNames)
        {
            var status = new Faker<BillingStatusDataAccessModel>()
                .RuleFor(b => b.Id, f => Guid.NewGuid())
                .RuleFor(b => b.Name, statusName)
                .RuleFor(b => b.CreatedAt, f => f.Date.Recent())
                .RuleFor(b => b.UpdatedAt, (f, b) => b.CreatedAt)
                .Generate();
    
            billingStatusesToSeed.Add(status);
        }

        // --- 3. Generate Thesis Statuses ---
        // Define the lifecycle stages of a thesis.
        var thesisStatusNames = new[] { "IN_DISCUSSION", "REGISTERED", "SUBMITTED", "DEFENDED" };
        var thesisStatusesToSeed = new List<ThesisStatusDataAccessModel>();

        foreach (var statusName in thesisStatusNames)
        {
            var status = new Faker<ThesisStatusDataAccessModel>()
                .RuleFor(t => t.Id, f => Guid.NewGuid())
                .RuleFor(t => t.Name, statusName)
                .RuleFor(t => t.CreatedAt, f => f.Date.Recent())
                .RuleFor(t => t.UpdatedAt, (f, t) => t.CreatedAt)
                .Generate();
    
            thesisStatusesToSeed.Add(status);
        }

        // --- 4. Generate Topics ---
        // Retrieve a predefined list of topics from the TopicForSeeding method.
        // These topics cover various fields like AI, Web Development, etc.
        var topicsFromMethod = TopicForSeeding();
        var topicsToSeed = new List<TopicDataAccessModel>();

        foreach (var topicItem in topicsFromMethod)
        {
            var topic = new Faker<TopicDataAccessModel>()
                .RuleFor(t => t.Id, f => Guid.NewGuid())
                .RuleFor(t => t.Title, topicItem.Title)
                .RuleFor(t => t.Description, topicItem.Description)
                .RuleFor(t => t.CreatedAt, f => f.Date.Recent())
                .RuleFor(t => t.UpdatedAt, (f, t) => t.CreatedAt)
                .Generate();
    
            topicsToSeed.Add(topic);
        }

        // --- 5. Generate Users ---
        // Create a Faker for UserSeed to generate random user data.
        // Passwords are hashed using BCrypt.
        var userFaker = new Faker<UserSeed>()
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Password, f => f.Internet.Password())
            .RuleFor(u => u.PasswordHash, (f, u) => BCrypt.Net.BCrypt.HashPassword(u.Password, workFactor: 4))
            .RuleFor(u => u.Id, f => Guid.NewGuid())
            .RuleFor(u => u.CreatedAt, f => f.Date.Past())
            .RuleFor(u => u.UpdatedAt, f => f.Date.Recent());
        
        // Generate 100 random users.
        var users = userFaker.Generate(100);

        // --- 6. Assign Roles to Users ---
        // Randomly assign one of the created roles to each generated user.
        var userRolesToSeed = new List<UserRoleDataAccessModel>();
        var faker = new Faker();

        foreach (var user in users)
        {
            var randomRole = faker.PickRandom(rolesToSeed);
            var userRole = new UserRoleDataAccessModel
            {
                UserId = user.Id,
                RoleId = randomRole.Id
            };
            userRolesToSeed.Add(userRole);
        }
        
        // --- 7. Create Specific Test Users ---
        // Create specific users (Abraham, Eddie, Stefan, Michael) with known credentials for manual testing.
        // Each tester gets created for every role (Student, Tutor, Admin).
        var testers = new[] { "Abraham", "Eddie", "Stefan", "Michael" };
        foreach (var testerName in testers)
        {
            var password = "password123";
            foreach (var role in rolesToSeed)
            {
                var specificUser = new UserSeed
                {
                    Id = Guid.NewGuid(),
                    FirstName = testerName,
                    LastName = role.Name, // Using role name as last name for easy identification (e.g., Abraham Tutor)
                    Email = $"{testerName.ToLower()}.{role.Name.ToLower()}@test.com",
                    Password = password,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, workFactor:4), // Fixed password for testing
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                users.Add(specificUser);

                var specificUserRole = new UserRoleDataAccessModel
                {
                    UserId = specificUser.Id,
                    RoleId = role.Id
                };
                userRolesToSeed.Add(specificUserRole);
            }
        }
        
        // --- 8. Identify Tutors ---
        // Filter the list of users to find all those who have been assigned the "TUTOR" role.
        var tutorRole = rolesToSeed.First(r => r.Name == "TUTOR");
        var tutorUserIds = userRolesToSeed.Where(ur => ur.RoleId == tutorRole.Id).Select(ur => ur.UserId);
        var tutors = users.Where(u => tutorUserIds.Contains(u.Id)).ToList();
        
        // --- 9. Assign Topics to Tutors ---
        // This section handles the N:M relationship between Tutors and Topics.
        var userTopicAssignments = new HashSet<(Guid UserId, Guid TopicId)>();

        // Step 9a: Assign a random number of topics (2 to 7) to each tutor.
        foreach (var tutor in tutors)
        {
            var numTopics = faker.Random.Int(2, 7);
            var topicsToAssign = faker.PickRandom(topicsToSeed, numTopics);
            
            foreach (var topic in topicsToAssign)
            {
                userTopicAssignments.Add((tutor.Id, topic.Id));
            }
        }

        // Step 9b: Ensure coverage - every topic must be covered by at least 4 tutors.
        // If a topic has fewer than 4 tutors, we assign additional tutors to it.
        foreach (var topic in topicsToSeed)
        {
            var assignmentsForTopic = userTopicAssignments.Count(ut => ut.TopicId == topic.Id);
            var needed = 4 - assignmentsForTopic;

            if (needed > 0)
            {
                // Find tutors who are NOT yet assigned to this topic.
                var assignedTutorIds = userTopicAssignments
                    .Where(ut => ut.TopicId == topic.Id)
                    .Select(ut => ut.UserId);
                    
                var assignableTutors = tutors.Where(t => !assignedTutorIds.Contains(t.Id)).ToList();
                
                // Pick the needed number of tutors from the available pool.
                var tutorsToAssign = faker.PickRandom(assignableTutors, Math.Min(needed, assignableTutors.Count));

                foreach (var tutor in tutorsToAssign)
                {
                    userTopicAssignments.Add((tutor.Id, topic.Id));
                }
            }
        }

        // Convert the assignments to the UserTopicDataAccessModel entity.
        var userTopicsToSeed = userTopicAssignments
            .Select(ut => new UserTopicDataAccessModel { UserId = ut.UserId, TopicId = ut.TopicId })
            .ToList();
        
        
        // --- 10. Generate Theses ---
        // Identify users with the "STUDENT" role to assign as thesis owners.
        var studentRole = rolesToSeed.First(r => r.Name == "STUDENT");
        var studentUserIds = userRolesToSeed.Where(ur => ur.RoleId == studentRole.Id).Select(ur => ur.UserId);
        var students = users.Where(u => studentUserIds.Contains(u.Id)).ToList();
        
        var thesesToSeed = new List<ThesisDataAccessModel>();
        var thesisFaker = new Faker<ThesisDataAccessModel>()
            .RuleFor(t => t.Id, f => Guid.NewGuid())
            .RuleFor(t => t.Title, f => f.Lorem.Sentence(3))
            // SubjectArea was removed from the model
            .RuleFor(t => t.StatusId, f => f.PickRandom(thesisStatusesToSeed).Id)
            .RuleFor(t => t.BillingStatusId, f => f.PickRandom(billingStatusesToSeed).Id)
            .RuleFor(t => t.OwnerId, f => f.PickRandom(students).Id)
            .RuleFor(t => t.CreatedAt, f => f.Date.Past())
            .RuleFor(t => t.UpdatedAt, f => f.Date.Recent());
        
        // Generate 40 theses with specific logic for tutor assignment.
        for (int i = 0; i < 40; i++)
        {
            var thesis = thesisFaker.Generate();
            
            // Step 10a: Pick a random topic for the thesis.
            var topic = faker.PickRandom(topicsToSeed);
            thesis.TopicId = topic.Id;
            
            // Step 10b: Find tutors who are actually assigned to cover this specific topic.
            // This ensures that the tutor has the expertise for the thesis topic.
            var eligibleTutorIds = userTopicAssignments
                .Where(ut => ut.TopicId == topic.Id)
                .Select(ut => ut.UserId)
                .ToList();
            
            // Step 10c: Randomly select one of the eligible tutors as the main supervisor.
            var tutorId = faker.PickRandom(eligibleTutorIds);
            thesis.TutorId = tutorId;
            
            // Step 10d: Pick a second supervisor. This can be any tutor except the main supervisor.
            var otherTutors = tutors.Where(t => t.Id != tutorId).ToList();
            thesis.SecondSupervisorId = faker.PickRandom(otherTutors).Id;
            
            thesesToSeed.Add(thesis);
        }

        // --- 11. Generate Thesis Documents ---
        // Create dummy PDF documents associated with the generated theses.
        var thesisDocumentsToSeed = new List<ThesisDocumentDataAccessModel>();
        var thesisDocumentFaker = new Faker<ThesisDocumentDataAccessModel>()
            .RuleFor(td => td.Id, f => Guid.NewGuid())
            .RuleFor(td => td.FileName, f => f.System.FileName("pdf"))
            .RuleFor(td => td.ContentType, "application/pdf")
            .RuleFor(td => td.Content, f => Encoding.UTF8.GetBytes("This is a thesis.")) // Dummy content
            .RuleFor(td => td.ThesisId, f => f.PickRandom(thesesToSeed).Id)
            .RuleFor(td => td.CreatedAt, f => f.Date.Past())
            .RuleFor(td => td.UpdatedAt, f => f.Date.Recent());
        
        thesisDocumentsToSeed.AddRange(thesisDocumentFaker.Generate(40));
        
        
        // --- 12. Serialize and Save ---
        // Combine all generated data into a single object.
        var seedData = new { Users = users, Roles = rolesToSeed, UserRoles = userRolesToSeed, BillingStatuses = billingStatusesToSeed, ThesisStatuses = thesisStatusesToSeed, Topics = topicsToSeed, UserTopics = userTopicsToSeed, Theses = thesesToSeed, ThesisDocuments = thesisDocumentsToSeed };
        
        // Serialize the data to JSON with indentation for readability.
        var json = JsonSerializer.Serialize(seedData, new JsonSerializerOptions { WriteIndented = true });
        
        // Write the JSON to a file named "seed.json" in the execution directory.
        File.WriteAllText(Path.Combine(directory, "seed.json"), json);

    }

    /// <summary>
    /// Helper method to provide a list of predefined topics for seeding.
    /// </summary>
    /// <returns>A list of TopicDataAccessModel objects.</returns>
    public List<TopicDataAccessModel> TopicForSeeding()
    {
                var topicsToSeed = new List<TopicDataAccessModel>
{
    new() { Title = "Künstliche Intelligenz (AI)", Description = "Methoden und Anwendungen zur Nachbildung intelligenter Verhaltensweisen durch Maschinen." },
    new() { Title = "Maschinelles Lernen", Description = "Selbstlernende Algorithmen zur Mustererkennung und Vorhersage." },
    new() { Title = "Deep Learning", Description = "Neuronale Netzwerke mit vielen Schichten für komplexe Lernaufgaben." },
    new() { Title = "Computer Vision", Description = "Automatisierte visuelle Wahrnehmung und Bilderkennung durch Computer." },
    new() { Title = "Natural Language Processing (NLP)", Description = "Verarbeitung und Analyse natürlicher Sprache durch Maschinen." },
    new() { Title = "Big Data & Analytics", Description = "Analyse großer Datenmengen zur Entscheidungsunterstützung." },
    new() { Title = "Data Mining", Description = "Extraktion von Mustern und Wissen aus großen Datensätzen." },
    new() { Title = "Mobile App Entwicklung (Android/iOS)", Description = "Entwicklung von mobilen Anwendungen für Android und iOS." },
    new() { Title = "Webentwicklung (Frontend/Backend)", Description = "Design und Implementierung moderner Webanwendungen." },
    new() { Title = "Softwarearchitektur", Description = "Strukturierung und Entwurf komplexer Softwaresysteme." },
    new() { Title = "Testautomatisierung", Description = "Automatisiertes Testen von Software zur Qualitätssteigerung." },
    new() { Title = "Datenbanken & SQL", Description = "Datenmodellierung und Datenabfragen mit relationalen Systemen." },
    new() { Title = "DevOps & CI/CD", Description = "Automatisierung und Integration im Softwareentwicklungsprozess." },
    new() { Title = "Cloud Computing (AWS, Azure, GCP)", Description = "Skalierbare Infrastruktur- und Plattformlösungen in der Cloud." },
    new() { Title = "IT-Sicherheit / Cybersecurity", Description = "Schutz von IT-Systemen gegen Angriffe und Bedrohungen." },
    new() { Title = "Blockchain-Technologie", Description = "Verteilte Systeme zur sicheren Datenübertragung und Speicherung." },
    new() { Title = "Edge Computing", Description = "Verarbeitung von Daten direkt an der Quelle zur Reduktion von Latenz." },
    new() { Title = "Embedded Systems", Description = "Hardwarenahe Softwareentwicklung für spezialisierte Geräte." },
    new() { Title = "AR/VR/XR-Anwendungen", Description = "Interaktive virtuelle und erweiterte Realitäten für Nutzererlebnisse." },
    new() { Title = "Game Development", Description = "Konzeption und Entwicklung digitaler Spiele." },
    
    new() { Title = "Digital Business Transformation", Description = "Digitale Umgestaltung von Geschäftsmodellen und -prozessen." },
    new() { Title = "E-Commerce", Description = "Online-Vertrieb von Produkten und Dienstleistungen." },
    new() { Title = "Prozessautomatisierung (RPA)", Description = "Automatisierung repetitiver Geschäftsprozesse mit Software-Bots." },
    new() { Title = "Controlling mit Power BI / Tableau", Description = "Visuelle Aufbereitung betrieblicher Kennzahlen." },
    new() { Title = "CRM-Systeme & Kundenanalyse", Description = "Verwaltung und Analyse von Kundenbeziehungen." },
    new() { Title = "IT-Projektmanagement", Description = "Planung und Steuerung von IT-Projekten." },
    new() { Title = "Innovationsmanagement", Description = "Förderung und Strukturierung von Innovationsprozessen." },
    new() { Title = "Wirtschaftsinformatik", Description = "Schnittstelle zwischen IT und BWL." },
    new() { Title = "Finanztechnologie (FinTech)", Description = "Technologien im Finanzdienstleistungssektor." },
    new() { Title = "Business Intelligence", Description = "Datengetriebene Geschäftsanalysen und Entscheidungsfindung." },

    new() { Title = "Wissenschaftliches Arbeiten mit LaTeX", Description = "Professionelles Erstellen von Abschlussarbeiten mit LaTeX." },
    new() { Title = "Interdisziplinäre Projektarbeit", Description = "Zusammenarbeit über Fachgrenzen hinweg in Projekten." },
    new() { Title = "Agile Methoden (Scrum, Kanban)", Description = "Agile Vorgehensweisen im Projekt- und Produktmanagement." },
    new() { Title = "Change Management", Description = "Begleitung von Veränderungsprozessen in Organisationen." },
    new() { Title = "Ethik in der KI", Description = "Gesellschaftliche Verantwortung und Fairness in Algorithmen." },
    new() { Title = "UX/UI Design", Description = "Benutzerzentriertes Gestalten digitaler Oberflächen." },
    new() { Title = "Accessibility & Inclusive Design", Description = "Barrierefreies Design digitaler Produkte." },
    new() { Title = "Smart Cities", Description = "Intelligente Städte durch IT-Infrastruktur." },
    new() { Title = "Nachhaltige IT", Description = "Umweltbewusste IT-Lösungen und Energieeffizienz." },
    new() { Title = "Medienrecht & Datenschutz (DSGVO)", Description = "Rechtsgrundlagen im Umgang mit digitalen Inhalten." },

    // Zusätzliche Themen in Informatik, Medien und Kommunikation, Wirtschaft usw.
    new() { Title = "Algorithmen und Datenstrukturen", Description = "Grundlagen effizienter Algorithmen und Datenorganisation." },
    new() { Title = "Betriebssysteme", Description = "Funktionsweise und Verwaltung von Betriebssystemen." },
    new() { Title = "Netzwerktechnik", Description = "Design und Sicherheit von Computernetzen." },
    new() { Title = "Software Engineering", Description = "Methoden zur systematischen Softwareentwicklung." },
    new() { Title = "Programmiersprachen (C#, Java, Python)", Description = "Vergleich und Anwendung populärer Programmiersprachen." },
    new() { Title = "Human-Computer Interaction", Description = "Interaktion zwischen Menschen und Computern." },
    new() { Title = "Robotik", Description = "Entwicklung und Steuerung von Robotern." },
    new() { Title = "Quantencomputing", Description = "Grundlagen und Anwendungen der Quanteninformationsverarbeitung." },
    new() { Title = "Internet of Things (IoT)", Description = "Vernetzung intelligenter Geräte und Sensoren." },
    new() { Title = "Mikroservices", Description = "Architektur für skalierbare und modulare Anwendungen." },
    new() { Title = "API-Entwicklung", Description = "Design und Implementierung von Application Programming Interfaces." },
    new() { Title = "Versionskontrolle (Git)", Description = "Verwaltung von Code-Änderungen mit Git." },
    new() { Title = "Containerisierung (Docker)", Description = "Isolierung und Bereitstellung von Anwendungen." },
    new() { Title = "Kubernetes", Description = "Orchestrierung von Container-Anwendungen." },
    new() { Title = "Reinforcement Learning", Description = "Lernalgorithmen durch Belohnung und Strafe." },
    new() { Title = "Computer Graphics", Description = "Erzeugung und Manipulation visueller Inhalte." },
    new() { Title = "Bioinformatik", Description = "Anwendung von IT in der Biologie und Medizin." },
    new() { Title = "Kryptographie", Description = "Sichere Verschlüsselung und Datenschutz." },
    new() { Title = "Ethik Hacking", Description = "Verantwortungsvolles Testen von Sicherheitssystemen." },
    new() { Title = "Digitale Forensik", Description = "Untersuchung digitaler Beweise." },
    new() { Title = "Cloud-Sicherheit", Description = "Schutz von Daten in Cloud-Umgebungen." },
    new() { Title = "Datenschutz", Description = "Grundlagen des Datenschutzes und Privatsphäre." },
    new() { Title = "Agile Entwicklung", Description = "Flexible Methoden in der Softwareentwicklung." },
    new() { Title = "Test-Driven Development (TDD)", Description = "Entwicklung durch automatisierte Tests." },
    new() { Title = "Behavior-Driven Development (BDD)", Description = "Spezifikation durch Verhaltensbeschreibungen." },
    new() { Title = "Monitoring und Logging", Description = "Überwachung und Protokollierung von Systemen." },
    new() { Title = "Performance-Optimierung", Description = "Verbesserung der Geschwindigkeit und Effizienz." },
    new() { Title = "Skalierbarkeit", Description = "Anpassung von Systemen an wachsende Anforderungen." },
    new() { Title = "Fehlertoleranz", Description = "Robustheit gegen Ausfälle und Fehler." },
    new() { Title = "Design Patterns", Description = "Bewährte Lösungen für Softwareprobleme." },
    new() { Title = "SOLID-Prinzipien", Description = "Grundregeln für sauberen Code." },
    new() { Title = "MVC-Architektur", Description = "Modell-View-Controller für Anwendungsstruktur." },
    new() { Title = "RESTful APIs", Description = "Entwicklung von REST-basierten Schnittstellen." },
    new() { Title = "GraphQL", Description = "Flexible Abfragesprache für APIs." },
    new() { Title = "WebSockets", Description = "Echtzeitkommunikation in Webanwendungen." },
    new() { Title = "Progressive Web Apps", Description = "Web-Apps mit nativer App-Funktionalität." },
    new() { Title = "Serverless Computing", Description = "Ausführung von Code ohne Serververwaltung." },
    new() { Title = "Event-Driven Architecture", Description = "Architektur basierend auf Ereignissen." },
    new() { Title = "Message Queues", Description = "Asynchrone Nachrichtenverarbeitung." },
    new() { Title = "NoSQL-Datenbanken", Description = "Nicht-relationale Datenbanksysteme." },
    new() { Title = "MongoDB", Description = "Dokumentenbasierte NoSQL-Datenbank." },
    new() { Title = "Redis", Description = "In-Memory-Datenstruktur-Speicher." },
    new() { Title = "Elasticsearch", Description = "Suchmaschine für Volltextsuche." },
    new() { Title = "Supply Chain Management", Description = "Optimierung von Lieferketten." },
    new() { Title = "Marketing Automation", Description = "Automatisierte Marketingkampagnen." },
    new() { Title = "Digital Marketing", Description = "Online-Marketingstrategien." },
    new() { Title = "SEO/SEM", Description = "Suchmaschinenoptimierung und -marketing." },
    new() { Title = "Social Media Marketing", Description = "Marketing über soziale Netzwerke." },
    new() { Title = "Content Management Systems", Description = "Systeme zur Verwaltung von Inhalten." },
    new() { Title = "ERP-Systeme", Description = "Enterprise Resource Planning." },
    new() { Title = "SAP", Description = "Unternehmenssoftware von SAP." },
    new() { Title = "Salesforce", Description = "CRM- und Vertriebsplattform." },
    new() { Title = "Data Warehousing", Description = "Zentralisierung von Daten für Analysen." },
    new() { Title = "ETL-Prozesse", Description = "Extraktion, Transformation und Laden von Daten." },
    new() { Title = "Predictive Analytics", Description = "Vorhersagemodelle mit Daten." },
    new() { Title = "Machine Learning in Business", Description = "KI-Anwendungen in Unternehmen." },
    new() { Title = "Blockchain in Finance", Description = "Kryptowährungen und Finanztransaktionen." },
    new() { Title = "Regulatorische Compliance", Description = "Einhaltung gesetzlicher Vorschriften." },
    new() { Title = "Risikomanagement", Description = "Identifikation und Minimierung von Risiken." },
    new() { Title = "Portfolio-Management", Description = "Verwaltung von Investitionsportfolios." },
    new() { Title = "Peer-to-Peer Lending", Description = "Direkte Kreditvergabe zwischen Personen." },
    new() { Title = "Crowdfunding", Description = "Finanzierung durch die Crowd." },
    new() { Title = "Insurance Technology (InsurTech)", Description = "Technologie in der Versicherungsbranche." },
    new() { Title = "HealthTech", Description = "Technologie im Gesundheitswesen." },
    new() { Title = "EdTech", Description = "Bildungstechnologie." },
    new() { Title = "PropTech", Description = "Immobilientechnologie." },
    new() { Title = "LegalTech", Description = "Technologie im Rechtsbereich." },
    new() { Title = "RegTech", Description = "Regulatorische Technologie." },
    new() { Title = "Digitale Medien", Description = "Erstellung und Verbreitung digitaler Inhalte." },
    new() { Title = "Social Media Strategien", Description = "Planung und Umsetzung von Social-Media-Kampagnen." },
    new() { Title = "Content Creation", Description = "Erstellung ansprechender Inhalte." },
    new() { Title = "Video Production", Description = "Produktion von Videoinhalten." },
    new() { Title = "Podcasting", Description = "Erstellung und Verbreitung von Podcasts." },
    new() { Title = "Journalismus im Digitalen Zeitalter", Description = "Moderne Nachrichtenproduktion." },
    new() { Title = "Public Relations", Description = "Öffentlichkeitsarbeit und Imagepflege." },
    new() { Title = "Werbung", Description = "Strategien und Techniken der Werbung." },
    new() { Title = "Branding", Description = "Aufbau und Pflege von Marken." },
    new() { Title = "Medienrecht", Description = "Rechtliche Aspekte der Medien." },
    new() { Title = "Urheberrecht", Description = "Schutz geistigen Eigentums." },
    new() { Title = "Kommunikationstheorie", Description = "Theorien der menschlichen Kommunikation." },
    new() { Title = "Massenkommunikation", Description = "Kommunikation mit großen Zielgruppen." },
    new() { Title = "Multimedia Design", Description = "Gestaltung interaktiver Medien." },
    new() { Title = "Grafikdesign", Description = "Visuelle Gestaltung von Inhalten." },
    new() { Title = "Fotografie", Description = "Techniken der digitalen Fotografie." },
    new() { Title = "Video Editing", Description = "Bearbeitung von Videomaterial." },
    new() { Title = "Animation", Description = "Erstellung bewegter Bilder." },
    new() { Title = "Virtual Reality in Media", Description = "VR-Anwendungen in der Medienbranche." },
    new() { Title = "Augmented Reality in Media", Description = "AR für interaktive Erlebnisse." },
    new() { Title = "Streaming Services", Description = "Plattformen für Video- und Audio-Streaming." },
    new() { Title = "Influencer Marketing", Description = "Marketing durch Influencer." },
    new() { Title = "Krisenkommunikation", Description = "Kommunikation in Krisensituationen." },
    new() { Title = "Corporate Communication", Description = "Unternehmensinterne und -externe Kommunikation." },
    new() { Title = "Event Management", Description = "Planung und Durchführung von Events." },
    new() { Title = "Pressemitteilungen", Description = "Verfassen und Verteilen von Pressetexten." },
    new() { Title = "Medienethik", Description = "Ethische Standards in den Medien." },
    new() { Title = "Fake News Detection", Description = "Erkennung und Bekämpfung von Falschinformationen." },
    new() { Title = "Data Journalism", Description = "Journalismus mit Datenanalyse." },
};

return topicsToSeed;

    }
    
    
}