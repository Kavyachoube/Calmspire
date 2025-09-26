using CalmSpire.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalmSpire.Data
{
    public static class DataInitializer
    {
        public static void SeedAssessments(CalmSpireDbContext db)
        {
            if (db.Assessments.Any()) return; // Agar pehle se data h to skip

            var assessments = new List<Assessment>
            {
                new Assessment {
                    Title = "Depression Test",
                    Description = "Check for common signs of depression.",
                    QuestionsJson = @"
                    [
                        {""Question"":""I feel down or hopeless often."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I have lost interest in activities I once enjoyed."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I feel tired or have little energy."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I have trouble concentrating."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I feel worthless or guilty."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]}
                    ]"
                },
                new Assessment {
                    Title = "Anxiety Test",
                    Description = "Evaluate your anxiety symptoms.",
                    QuestionsJson = @"
                    [
                        {""Question"":""I worry excessively about different things."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I feel nervous or on edge."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I experience racing thoughts."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I avoid situations due to fear or worry."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I have physical symptoms like sweating or trembling."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]}
                    ]"
                },
                new Assessment {
                    Title = "Stress Level Test",
                    Description = "Measure your current stress levels.",
                    QuestionsJson = @"
                    [
                        {""Question"":""I find it hard to relax."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I feel overwhelmed by responsibilities."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I get irritated easily."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I have headaches or muscle tension."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I feel like I can’t cope with daily demands."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]}
                    ]"
                },
                new Assessment {
                    Title = "Sleep Quality Test",
                    Description = "Assess the quality of your sleep habits.",
                    QuestionsJson = @"
                    [
                        {""Question"":""I have trouble falling asleep."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I wake up during the night."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I wake up feeling tired or unrested."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I sleep fewer than 6 hours on most nights."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I rely on caffeine to stay awake during the day."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]}
                    ]"
                },
                new Assessment {
                    Title = "Mindfulness Test",
                    Description = "Check your ability to stay present and mindful.",
                    QuestionsJson = @"
                    [
                        {""Question"":""I focus on what I am doing in the present moment."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I notice small details in daily life."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I eat meals without distractions like phone/TV."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I reflect on my thoughts calmly."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I take mindful breaks during work/study."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]}
                    ]"
                },
                new Assessment {
                    Title = "Burnout Test",
                    Description = "Check if you may be experiencing burnout symptoms.",
                    QuestionsJson = @"
                    [
                        {""Question"":""I feel exhausted even after resting."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I feel detached from work or studies."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I have trouble staying motivated."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I feel cynical about my responsibilities."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I experience frequent headaches or fatigue."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]}
                    ]"
                },
                new Assessment {
                    Title = "Self-Esteem Test",
                    Description = "Evaluate your confidence and self-worth.",
                    QuestionsJson = @"
                    [
                        {""Question"":""I feel confident in my abilities."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I compare myself negatively to others."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I accept compliments easily."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I feel proud of my achievements."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I believe I am worthy of respect."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]}
                    ]"
                },
                new Assessment {
                    Title = "Social Anxiety Test",
                    Description = "Measure your social comfort and anxiety levels.",
                    QuestionsJson = @"
                    [
                        {""Question"":""I avoid social situations due to fear."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I feel judged in social interactions."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I experience anxiety before public speaking."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I have difficulty making eye contact."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I replay conversations in my head after they happen."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]}
                    ]"
                },
                new Assessment {
                    Title = "Anger Management Test",
                    Description = "Evaluate your anger control and reactions.",
                    QuestionsJson = @"
                    [
                        {""Question"":""I lose my temper quickly."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I regret my words/actions when angry."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I shout or raise my voice easily."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I hold grudges for a long time."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I struggle to calm down after being angry."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]}
                    ]"
                },
                new Assessment {
                    Title = "Happiness Test",
                    Description = "Gauge your current level of happiness.",
                    QuestionsJson = @"
                    [
                        {""Question"":""I feel satisfied with my life."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I experience joy in daily activities."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I feel optimistic about the future."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I feel connected to people around me."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]},
                        {""Question"":""I enjoy small moments of life."",""Type"":""radio"",""Options"":[""Never"",""Sometimes"",""Often"",""Always""]}
                    ]"
                }
            };

            db.Assessments.AddRange(assessments);
            db.SaveChanges();
        }
    }
}
