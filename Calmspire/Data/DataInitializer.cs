using CalmSpire.Data;
using CalmSpire.Models;

namespace CalmSpire.Data
{
    public static class DataInitializer
    {
        public static void SeedAssessments(CalmSpireDbContext db)
        {
            if (db.Assessments.Any()) return; // agar pehle se data h to dobara add na ho

            var assessments = new List<Assessment>
            {
                new Assessment {
                    Title = "Depression Test",
                    Description = "Check for common signs of depression.",
                    QuestionsJson = "[{\"Question\":\"I feel down or hopeless often.\",\"Type\":\"radio\",\"Options\":[\"Never\",\"Sometimes\",\"Often\",\"Always\"]},{\"Question\":\"I have lost interest in activities I once enjoyed.\",\"Type\":\"radio\",\"Options\":[\"Never\",\"Sometimes\",\"Often\",\"Always\"]}]"
                },
                new Assessment {
                    Title = "Anxiety Test",
                    Description = "Evaluate your anxiety symptoms.",
                    QuestionsJson = "[{\"Question\":\"I worry excessively about different things.\",\"Type\":\"radio\",\"Options\":[\"Never\",\"Sometimes\",\"Often\",\"Always\"]},{\"Question\":\"I feel nervous or on edge.\",\"Type\":\"radio\",\"Options\":[\"Never\",\"Sometimes\",\"Often\",\"Always\"]}]"
                },
                new Assessment {
                    Title = "Stress Level Test",
                    Description = "Measure your current stress levels.",
                    QuestionsJson = "[{\"Question\":\"I find it hard to relax.\",\"Type\":\"radio\",\"Options\":[\"Never\",\"Sometimes\",\"Often\",\"Always\"]},{\"Question\":\"I feel overwhelmed by responsibilities.\",\"Type\":\"radio\",\"Options\":[\"Never\",\"Sometimes\",\"Often\",\"Always\"]}]"
                },
                new Assessment {
                    Title = "Sleep Quality Test",
                    Description = "Assess the quality of your sleep habits.",
                    QuestionsJson = "[{\"Question\":\"I have trouble falling asleep.\",\"Type\":\"radio\",\"Options\":[\"Never\",\"Sometimes\",\"Often\",\"Always\"]},{\"Question\":\"I wake up feeling rested.\",\"Type\":\"radio\",\"Options\":[\"Never\",\"Sometimes\",\"Often\",\"Always\"]}]"
                },
                new Assessment {
                    Title = "Mindfulness Test",
                    Description = "Check your ability to stay present and mindful.",
                    QuestionsJson = "[{\"Question\":\"I focus on what I am doing in the present moment.\",\"Type\":\"radio\",\"Options\":[\"Never\",\"Sometimes\",\"Often\",\"Always\"]},{\"Question\":\"I notice small details in daily life.\",\"Type\":\"radio\",\"Options\":[\"Never\",\"Sometimes\",\"Often\",\"Always\"]}]"
                },
                new Assessment {
                    Title = "Burnout Test",
                    Description = "Check if you may be experiencing burnout symptoms.",
                    QuestionsJson = "[{\"Question\":\"I feel exhausted even after resting.\",\"Type\":\"radio\",\"Options\":[\"Never\",\"Sometimes\",\"Often\",\"Always\"]},{\"Question\":\"I feel detached from work or studies.\",\"Type\":\"radio\",\"Options\":[\"Never\",\"Sometimes\",\"Often\",\"Always\"]}]"
                },
                new Assessment {
                    Title = "Self-Esteem Test",
                    Description = "Evaluate your confidence and self-worth.",
                    QuestionsJson = "[{\"Question\":\"I feel confident in my abilities.\",\"Type\":\"radio\",\"Options\":[\"Never\",\"Sometimes\",\"Often\",\"Always\"]},{\"Question\":\"I compare myself negatively to others.\",\"Type\":\"radio\",\"Options\":[\"Never\",\"Sometimes\",\"Often\",\"Always\"]}]"
                },
                new Assessment {
                    Title = "Social Anxiety Test",
                    Description = "Measure your social comfort and anxiety levels.",
                    QuestionsJson = "[{\"Question\":\"I avoid social situations due to fear.\",\"Type\":\"radio\",\"Options\":[\"Never\",\"Sometimes\",\"Often\",\"Always\"]},{\"Question\":\"I feel judged in social interactions.\",\"Type\":\"radio\",\"Options\":[\"Never\",\"Sometimes\",\"Often\",\"Always\"]}]"
                },
                new Assessment {
                    Title = "Anger Management Test",
                    Description = "Evaluate your anger control and reactions.",
                    QuestionsJson = "[{\"Question\":\"I lose my temper quickly.\",\"Type\":\"radio\",\"Options\":[\"Never\",\"Sometimes\",\"Often\",\"Always\"]},{\"Question\":\"I regret my words/actions when angry.\",\"Type\":\"radio\",\"Options\":[\"Never\",\"Sometimes\",\"Often\",\"Always\"]}]"
                },
                new Assessment {
                    Title = "Happiness Test",
                    Description = "Gauge your current level of happiness.",
                    QuestionsJson = "[{\"Question\":\"I feel satisfied with my life.\",\"Type\":\"radio\",\"Options\":[\"Never\",\"Sometimes\",\"Often\",\"Always\"]},{\"Question\":\"I experience joy in daily activities.\",\"Type\":\"radio\",\"Options\":[\"Never\",\"Sometimes\",\"Often\",\"Always\"]}]"
                }
            };

            db.Assessments.AddRange(assessments);
            db.SaveChanges();
        }
    }
}
