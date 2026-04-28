using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RoadSafetyAPI.Models;

namespace RoadSafetyAPI.Data;

public static class DataSeeder
{
    public static void Seed(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Database.EnsureCreated();

        if (context.Users.Any())
            return;

        // Admin
        var admin = new User
        {
            Name = "Admin User",
            Email = "admin@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            Role = "ADMIN"
        };
        context.Users.Add(admin);

        // Student 1
        var student1User = new User
        {
            Name = "Student One",
            Email = "student1@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student@123"),
            Role = "STUDENT"
        };
        context.Users.Add(student1User);

        // Student 2
        var student2User = new User
        {
            Name = "Student Two",
            Email = "student2@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student@123"),
            Role = "STUDENT"
        };
        context.Users.Add(student2User);

        // Parent
        var parentUser = new User
        {
            Name = "Parent User",
            Email = "parent@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Parent@123"),
            Role = "PARENT"
        };
        context.Users.Add(parentUser);

        context.SaveChanges();

        // Student Profiles
        var studentProfile1 = new StudentProfile
        {
            UserId = student1User.Id,
            FirstName = "Alice",
            Age = 10,
            Avatar = "avatar1.png",
            Points = 25,
            Level = 1
        };
        context.StudentProfiles.Add(studentProfile1);

        var studentProfile2 = new StudentProfile
        {
            UserId = student2User.Id,
            FirstName = "Bob",
            Age = 11,
            Avatar = "avatar2.png",
            Points = 60,
            Level = 2
        };
        context.StudentProfiles.Add(studentProfile2);

        // Parent Profile
        var parentProfile = new ParentProfile
        {
            UserId = parentUser.Id,
            Phone = "+33612345678"
        };
        context.ParentProfiles.Add(parentProfile);

        context.SaveChanges();

        // Badges
        var bronzeBadge = new Badge { Name = "Bronze", Description = "Earn at least 10 points", Type = "badge", Image = "bronze.png", MinPoints = 10 };
        var silverBadge = new Badge { Name = "Silver", Description = "Earn at least 50 points", Type = "badge", Image = "silver.png", MinPoints = 50 };
        var goldBadge = new Badge { Name = "Gold", Description = "Earn at least 100 points", Type = "trophy", Image = "gold.png", MinPoints = 100 };
        context.Badges.AddRange(bronzeBadge, silverBadge, goldBadge);
        context.SaveChanges();

        // Quiz 1: Road Signs Basics (Level 1)
        var quiz1 = new Quiz { Title = "Road Signs Basics", Description = "Learn the meaning of common road signs.", Level = 1, ScoreMax = 30 };
        context.Quizzes.Add(quiz1);
        context.SaveChanges();

        var q1_1 = new Question { QuizId = quiz1.Id, Content = "What does a red octagonal sign mean?", Type = "multiple-choice", Explication = "A red octagon is always a STOP sign — you must come to a complete stop." };
        var q1_2 = new Question { QuizId = quiz1.Id, Content = "What color is a warning sign typically?", Type = "multiple-choice", Explication = "Warning signs use a yellow background to alert drivers of potential hazards." };
        var q1_3 = new Question { QuizId = quiz1.Id, Content = "What does a green sign indicate?", Type = "multiple-choice", Explication = "Green signs provide directional and distance information." };
        context.Questions.AddRange(q1_1, q1_2, q1_3);
        context.SaveChanges();

        context.Answers.AddRange(
            new Answer { QuestionId = q1_1.Id, Content = "Stop", IsCorrect = true },
            new Answer { QuestionId = q1_1.Id, Content = "Yield", IsCorrect = false },
            new Answer { QuestionId = q1_1.Id, Content = "Speed up", IsCorrect = false },
            new Answer { QuestionId = q1_1.Id, Content = "Turn right", IsCorrect = false },

            new Answer { QuestionId = q1_2.Id, Content = "Yellow", IsCorrect = true },
            new Answer { QuestionId = q1_2.Id, Content = "Red", IsCorrect = false },
            new Answer { QuestionId = q1_2.Id, Content = "Blue", IsCorrect = false },
            new Answer { QuestionId = q1_2.Id, Content = "Green", IsCorrect = false },

            new Answer { QuestionId = q1_3.Id, Content = "Direction and guidance", IsCorrect = true },
            new Answer { QuestionId = q1_3.Id, Content = "Danger ahead", IsCorrect = false },
            new Answer { QuestionId = q1_3.Id, Content = "Stop immediately", IsCorrect = false },
            new Answer { QuestionId = q1_3.Id, Content = "No entry", IsCorrect = false }
        );
        context.SaveChanges();

        context.Corrections.AddRange(
            new Correction { QuestionId = q1_1.Id, Text = "A red octagonal sign always means STOP. You must come to a complete stop before proceeding.", Image = "", Video = "" },
            new Correction { QuestionId = q1_2.Id, Text = "Warning signs have a yellow background to alert drivers of upcoming hazards.", Image = "", Video = "" },
            new Correction { QuestionId = q1_3.Id, Text = "Green signs indicate direction, distance, and guidance information.", Image = "", Video = "" }
        );

        // Quiz 2: Traffic Rules (Level 2)
        var quiz2 = new Quiz { Title = "Traffic Rules", Description = "Test your knowledge of basic traffic rules.", Level = 2, ScoreMax = 30 };
        context.Quizzes.Add(quiz2);
        context.SaveChanges();

        var q2_1 = new Question { QuizId = quiz2.Id, Content = "At a four-way stop, who has the right of way?", Type = "multiple-choice", Explication = "The vehicle that arrived first has the right of way at a four-way stop." };
        var q2_2 = new Question { QuizId = quiz2.Id, Content = "What should you do when an emergency vehicle approaches?", Type = "multiple-choice", Explication = "Always pull over to the right and stop to allow emergency vehicles to pass." };
        var q2_3 = new Question { QuizId = quiz2.Id, Content = "What is the standard speed limit in a school zone?", Type = "multiple-choice", Explication = "School zone speed limits are typically 25 km/h or as posted on signs." };
        context.Questions.AddRange(q2_1, q2_2, q2_3);
        context.SaveChanges();

        context.Answers.AddRange(
            new Answer { QuestionId = q2_1.Id, Content = "The vehicle that arrived first", IsCorrect = true },
            new Answer { QuestionId = q2_1.Id, Content = "The vehicle on the left", IsCorrect = false },
            new Answer { QuestionId = q2_1.Id, Content = "The largest vehicle", IsCorrect = false },
            new Answer { QuestionId = q2_1.Id, Content = "The vehicle going straight", IsCorrect = false },

            new Answer { QuestionId = q2_2.Id, Content = "Pull over to the right and stop", IsCorrect = true },
            new Answer { QuestionId = q2_2.Id, Content = "Speed up to get out of the way", IsCorrect = false },
            new Answer { QuestionId = q2_2.Id, Content = "Continue driving normally", IsCorrect = false },
            new Answer { QuestionId = q2_2.Id, Content = "Stop in the middle of the road", IsCorrect = false },

            new Answer { QuestionId = q2_3.Id, Content = "25 km/h or as posted", IsCorrect = true },
            new Answer { QuestionId = q2_3.Id, Content = "60 km/h", IsCorrect = false },
            new Answer { QuestionId = q2_3.Id, Content = "80 km/h", IsCorrect = false },
            new Answer { QuestionId = q2_3.Id, Content = "50 km/h", IsCorrect = false }
        );
        context.SaveChanges();

        context.Corrections.AddRange(
            new Correction { QuestionId = q2_1.Id, Text = "At a four-way stop, the vehicle that arrived first has the right of way.", Image = "", Video = "" },
            new Correction { QuestionId = q2_2.Id, Text = "Pull over to the right and stop to allow emergency vehicles to pass safely.", Image = "", Video = "" },
            new Correction { QuestionId = q2_3.Id, Text = "School zone speed limits are 25 km/h or as posted to protect children.", Image = "", Video = "" }
        );

        // Quiz 3: Safety Procedures (Level 3)
        var quiz3 = new Quiz { Title = "Safety Procedures", Description = "Learn essential safety procedures for pedestrians.", Level = 3, ScoreMax = 30 };
        context.Quizzes.Add(quiz3);
        context.SaveChanges();

        var q3_1 = new Question { QuizId = quiz3.Id, Content = "What should you do before crossing the street?", Type = "multiple-choice", Explication = "Always look left, right, then left again before crossing to check for traffic from all directions." };
        var q3_2 = new Question { QuizId = quiz3.Id, Content = "Where should children wait for the school bus?", Type = "multiple-choice", Explication = "Children should wait on the sidewalk away from the road to stay safe." };
        var q3_3 = new Question { QuizId = quiz3.Id, Content = "What does a pedestrian crossing sign look like?", Type = "multiple-choice", Explication = "A pedestrian crossing sign shows a figure of a person walking on stripes." };
        context.Questions.AddRange(q3_1, q3_2, q3_3);
        context.SaveChanges();

        context.Answers.AddRange(
            new Answer { QuestionId = q3_1.Id, Content = "Look left, right, then left again", IsCorrect = true },
            new Answer { QuestionId = q3_1.Id, Content = "Run quickly across", IsCorrect = false },
            new Answer { QuestionId = q3_1.Id, Content = "Only look left", IsCorrect = false },
            new Answer { QuestionId = q3_1.Id, Content = "Cross only when cars are nearby", IsCorrect = false },

            new Answer { QuestionId = q3_2.Id, Content = "On the sidewalk away from the road", IsCorrect = true },
            new Answer { QuestionId = q3_2.Id, Content = "In the middle of the road", IsCorrect = false },
            new Answer { QuestionId = q3_2.Id, Content = "At the edge of the curb", IsCorrect = false },
            new Answer { QuestionId = q3_2.Id, Content = "Behind parked cars", IsCorrect = false },

            new Answer { QuestionId = q3_3.Id, Content = "A person walking on stripes", IsCorrect = true },
            new Answer { QuestionId = q3_3.Id, Content = "A red circle with a line", IsCorrect = false },
            new Answer { QuestionId = q3_3.Id, Content = "A yellow triangle", IsCorrect = false },
            new Answer { QuestionId = q3_3.Id, Content = "A blue square", IsCorrect = false }
        );
        context.SaveChanges();

        context.Corrections.AddRange(
            new Correction { QuestionId = q3_1.Id, Text = "Always look left, right, then left again before crossing the street.", Image = "", Video = "" },
            new Correction { QuestionId = q3_2.Id, Text = "Children must wait on the sidewalk, away from the road, for the school bus.", Image = "", Video = "" },
            new Correction { QuestionId = q3_3.Id, Text = "A pedestrian crossing sign shows a person walking on stripes (zebra crossing).", Image = "", Video = "" }
        );

        context.SaveChanges();

        // Videos
        context.Videos.AddRange(
            new Video
            {
                Title = "Introduction to Road Safety",
                Url = "https://www.youtube.com/watch?v=example1",
                Description = "An introductory video about road safety rules for children."
            },
            new Video
            {
                Title = "Understanding Traffic Signs",
                Url = "https://www.youtube.com/watch?v=example2",
                Description = "Learn about common traffic signs and their meanings."
            },
            new Video
            {
                Title = "Safe Pedestrian Practices",
                Url = "https://www.youtube.com/watch?v=example3",
                Description = "How to safely walk near roads and cross streets."
            }
        );

        // Safety Tips
        context.SafetyTips.AddRange(
            new SafetyTip { Title = "Look Both Ways", Description = "Always look both ways before crossing the street.", Type = "pedestrian", Image = "", Video = "" },
            new SafetyTip { Title = "Be Visible at Night", Description = "Wear bright or reflective clothing when walking near roads at night.", Type = "pedestrian", Image = "", Video = "" },
            new SafetyTip { Title = "No Running Near Roads", Description = "Never run into the road without checking for traffic first.", Type = "pedestrian", Image = "", Video = "" },
            new SafetyTip { Title = "Use Crossings", Description = "Always use designated pedestrian crossings.", Type = "pedestrian", Image = "", Video = "" },
            new SafetyTip { Title = "Stay on the Sidewalk", Description = "Stay on the sidewalk and away from the road edge.", Type = "pedestrian", Image = "", Video = "" }
        );

        // Parking Zones
        context.ParkingZones.AddRange(
            new ParkingZone
            {
                SchoolName = "Springfield Elementary",
                ZoneName = "Main Entrance Zone",
                Type = "ALLOWED",
                Location = "123 Main Street, Springfield",
                Description = "Designated drop-off zone in front of the main entrance.",
                Latitude = 37.7749,
                Longitude = -122.4194
            },
            new ParkingZone
            {
                SchoolName = "Riverside High School",
                ZoneName = "Side Street Restriction",
                Type = "FORBIDDEN",
                Location = "456 River Road, Springfield",
                Description = "Parking forbidden during school hours 7am-4pm.",
                Latitude = 37.7750,
                Longitude = -122.4180
            },
            new ParkingZone
            {
                SchoolName = "Oakwood Primary",
                ZoneName = "Back Parking Lot",
                Type = "ALLOWED",
                Location = "789 Oak Avenue, Springfield",
                Description = "Parent parking lot at the rear of the school.",
                Latitude = 37.7760,
                Longitude = -122.4200
            }
        );

        // Challenges (Defis)
        context.Defis.AddRange(
            new Defi
            {
                Title = "Road Sign Master",
                Description = "Complete the Road Signs Basics quiz with a perfect score.",
                Objective = "Score 100% on the Road Signs Basics quiz",
                PointsGain = 50
            },
            new Defi
            {
                Title = "Traffic Law Expert",
                Description = "Complete all level 2 quizzes.",
                Objective = "Complete the Traffic Rules quiz",
                PointsGain = 75
            },
            new Defi
            {
                Title = "Safety Champion",
                Description = "Earn the Silver badge by accumulating 50 points.",
                Objective = "Reach 50 total points",
                PointsGain = 100
            }
        );

        context.SaveChanges();
    }
}

