using Microsoft.EntityFrameworkCore;
using Surveillance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surveillance.Infrastructure.Persistence.Seeds
{
   

// =======================================================
// LMIS MASTER DATA SEEDER
// Covers:
// - Diseases
// - Symptoms
// - Lab Tests
// - Sample Types
// - Disease ↔ Symptoms
// - Disease ↔ LabTests
// - LabTest ↔ SampleTypes
// =======================================================

using Surveillance.Domain.Entities;


    public static class LMISSeedData
    {
        public static void Seed(ModelBuilder builder)
        {
            // ===================================================
            // SAMPLE TYPES
            // ===================================================


            builder.Entity<SampleType>().HasData(

                new SampleType { Id = 1, Name = "Blood" },
                new SampleType { Id = 2, Name = "Serum" },
                new SampleType { Id = 3, Name = "Urine" },
                new SampleType { Id = 4, Name = "Stool" },
                new SampleType { Id = 5, Name = "Swab" },
                new SampleType { Id = 6, Name = "CSF" },
                new SampleType { Id = 7, Name = "Sputum" },
                new SampleType { Id = 8, Name = "Skin Scraping" },
                new SampleType { Id = 9, Name = "Biopsy" }
            );

            // ===================================================
            // SYMPTOMS
            // ===================================================

            builder.Entity<Symptom>().HasData(

                new Symptom { Id = 1, Name = "Fever" },
                new Symptom { Id = 2, Name = "Headache" },
                new Symptom { Id = 3, Name = "Rash" },
                new Symptom { Id = 4, Name = "Vomiting" },
                new Symptom { Id = 5, Name = "Diarrhea" },
                new Symptom { Id = 6, Name = "Cough" },
                new Symptom { Id = 7, Name = "Breathlessness" },
                new Symptom { Id = 8, Name = "Body Pain" },
                new Symptom { Id = 9, Name = "Joint Pain" },
                new Symptom { Id = 10, Name = "Bleeding" },
                new Symptom { Id = 11, Name = "Jaundice" },
                new Symptom { Id = 12, Name = "Abdominal Pain" },
                new Symptom { Id = 13, Name = "Neck Rigidity" },
                new Symptom { Id = 14, Name = "Seizure" },
                new Symptom { Id = 15, Name = "Paralysis" },
                new Symptom { Id = 16, Name = "Weight Loss" },
                new Symptom { Id = 17, Name = "Night Sweats" },
                new Symptom { Id = 18, Name = "Lymph Node Swelling" },
                new Symptom { Id = 19, Name = "Skin Lesions" },
                new Symptom { Id = 20, Name = "Sore Throat" },
                new Symptom { Id = 21, Name = "Conjunctivitis" },
                new Symptom { Id = 22, Name = "Fatigue" },
                new Symptom { Id = 23, Name = "Loss of Appetite" },
                new Symptom { Id = 24, Name = "Chills" },
                new Symptom { Id = 25, Name = "Chest Pain" }
            );

            // ===================================================
            // LAB TESTS
            // ===================================================

            var seedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            builder.Entity<LabTest>().HasData(
                new LabTest { Id = 1, Name = "CBC", ShortCode = "CBC", Description = "Complete Blood Count", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 24, CreatedAt = seedDate },
                new LabTest { Id = 2, Name = "Dengue NS1", ShortCode = "DNS1", Description = "Dengue NS1 Antigen Test", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 24, CreatedAt = seedDate },
                new LabTest { Id = 3, Name = "Dengue IgM", ShortCode = "DIGM", Description = "Dengue IgM Antibody Test", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 48, CreatedAt = seedDate },
                new LabTest { Id = 4, Name = "Peripheral Smear", ShortCode = "PS", Description = "Peripheral Blood Smear", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 24, CreatedAt = seedDate },
                new LabTest { Id = 5, Name = "Malaria Antigen", ShortCode = "MAG", Description = "Malaria Rapid Antigen Test", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 24, CreatedAt = seedDate },
                new LabTest { Id = 6, Name = "RTPCR", ShortCode = "RTPCR", Description = "Real-Time PCR Test", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 48, CreatedAt = seedDate },
                new LabTest { Id = 7, Name = "Rapid Antigen Test", ShortCode = "RAT", Description = "Rapid Antigen Test", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 24, CreatedAt = seedDate },
                new LabTest { Id = 8, Name = "Widal Test", ShortCode = "WIDAL", Description = "Widal Test for Typhoid", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 24, CreatedAt = seedDate },
                new LabTest { Id = 9, Name = "Blood Culture", ShortCode = "BCULT", Description = "Blood Culture Test", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 72, CreatedAt = seedDate },
                new LabTest { Id = 10, Name = "Sputum AFB", ShortCode = "AFB", Description = "Sputum Acid Fast Bacilli Test", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 48, CreatedAt = seedDate },
                new LabTest { Id = 11, Name = "CBNAAT", ShortCode = "CBNAAT", Description = "Cartridge Based Nucleic Acid Amplification Test", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 48, CreatedAt = seedDate },
                new LabTest { Id = 12, Name = "CSF Analysis", ShortCode = "CSF", Description = "Cerebrospinal Fluid Analysis", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 24, CreatedAt = seedDate },
                new LabTest { Id = 13, Name = "ELISA", ShortCode = "ELISA", Description = "Enzyme-Linked Immunosorbent Assay", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 48, CreatedAt = seedDate },
                new LabTest { Id = 14, Name = "LFT", ShortCode = "LFT", Description = "Liver Function Test", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 24, CreatedAt = seedDate },
                new LabTest { Id = 15, Name = "KFT", ShortCode = "KFT", Description = "Kidney Function Test", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 24, CreatedAt = seedDate },
                new LabTest { Id = 16, Name = "Stool Culture", ShortCode = "SCULT", Description = "Stool Culture Test", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 72, CreatedAt = seedDate },
                new LabTest { Id = 17, Name = "Urine Routine", ShortCode = "URINE", Description = "Urine Routine Examination", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 24, CreatedAt = seedDate },
                new LabTest { Id = 18, Name = "Measles IgM", ShortCode = "MIGM", Description = "Measles IgM Antibody Test", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 48, CreatedAt = seedDate },
                new LabTest { Id = 19, Name = "Rubella IgM", ShortCode = "RIGM", Description = "Rubella IgM Antibody Test", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 48, CreatedAt = seedDate },
                new LabTest { Id = 20, Name = "HIV ELISA", ShortCode = "HIV", Description = "HIV ELISA Test", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 48, CreatedAt = seedDate },
                new LabTest { Id = 21, Name = "Hepatitis B Surface Antigen", ShortCode = "HBSAG", Description = "HBsAg Test", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 48, CreatedAt = seedDate },
                new LabTest { Id = 22, Name = "Hepatitis C Antibody", ShortCode = "HCV", Description = "HCV Antibody Test", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 48, CreatedAt = seedDate },
                new LabTest { Id = 23, Name = "Leptospira IgM", ShortCode = "LEPIGM", Description = "Leptospira IgM Antibody Test", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 48, CreatedAt = seedDate },
                new LabTest { Id = 24, Name = "Scrub Typhus IgM", ShortCode = "STIGM", Description = "Scrub Typhus IgM Antibody Test", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 48, CreatedAt = seedDate },
                new LabTest { Id = 25, Name = "JE IgM ELISA", ShortCode = "JEIGM", Description = "Japanese Encephalitis IgM ELISA", IsActive = true, RequiresVerification = true, ExpectedTurnaroundHours = 48, CreatedAt = seedDate }
            );

            // ===================================================
            // DISEASES
            // ===================================================

            builder.Entity<Disease>().HasData(

                new Disease { Id = 1, Name = "Dengue", IsNotifiable = true },
                new Disease { Id = 2, Name = "Malaria", IsNotifiable = true },
                new Disease { Id = 3, Name = "Chikungunya", IsNotifiable = true },
                new Disease { Id = 4, Name = "COVID-19", IsNotifiable = true },
                new Disease { Id = 5, Name = "Influenza", IsNotifiable = true },
                new Disease { Id = 6, Name = "Tuberculosis", IsNotifiable = true },
                new Disease { Id = 7, Name = "Typhoid", IsNotifiable = true },
                new Disease { Id = 8, Name = "Cholera", IsNotifiable = true },
                new Disease { Id = 9, Name = "Measles", IsNotifiable = true },
                new Disease { Id = 10, Name = "Rubella", IsNotifiable = true },
                new Disease { Id = 11, Name = "Meningitis", IsNotifiable = true },
                new Disease { Id = 12, Name = "AES/JE", IsNotifiable = true },
                new Disease { Id = 13, Name = "Hepatitis B", IsNotifiable = true },
                new Disease { Id = 14, Name = "Hepatitis C", IsNotifiable = true },
                new Disease { Id = 15, Name = "Leptospirosis", IsNotifiable = true },
                new Disease { Id = 16, Name = "Scrub Typhus", IsNotifiable = true },
                new Disease { Id = 17, Name = "Rabies", IsNotifiable = true },
                new Disease { Id = 18, Name = "Kala-azar", IsNotifiable = true },
                new Disease { Id = 19, Name = "Filariasis", IsNotifiable = true },
                new Disease { Id = 20, Name = "Leprosy", IsNotifiable = true },
                new Disease { Id = 21, Name = "HIV/AIDS", IsNotifiable = true }
            );

            // ===================================================
            // DISEASE ↔ SYMPTOM MAPPINGS
            // ===================================================

            builder.Entity<DiseaseSymptom>().HasData(

                // Dengue
                new DiseaseSymptom { DiseaseId = 1, SymptomId = 1, Weight = 10 },
                new DiseaseSymptom { DiseaseId = 1, SymptomId = 2, Weight = 8 },
                new DiseaseSymptom { DiseaseId = 1, SymptomId = 8, Weight = 8 },
                new DiseaseSymptom { DiseaseId = 1, SymptomId = 10, Weight = 9 },

                // Malaria
                new DiseaseSymptom { DiseaseId = 2, SymptomId = 1, Weight = 10 },
                new DiseaseSymptom { DiseaseId = 2, SymptomId = 24, Weight = 9 },
                new DiseaseSymptom { DiseaseId = 2, SymptomId = 22, Weight = 7 },

                // COVID
                new DiseaseSymptom { DiseaseId = 4, SymptomId = 1, Weight = 8 },
                new DiseaseSymptom { DiseaseId = 4, SymptomId = 6, Weight = 10 },
                new DiseaseSymptom { DiseaseId = 4, SymptomId = 7, Weight = 10 },
                new DiseaseSymptom { DiseaseId = 4, SymptomId = 22, Weight = 7 },

                // TB
                new DiseaseSymptom { DiseaseId = 6, SymptomId = 6, Weight = 10 },
                new DiseaseSymptom { DiseaseId = 6, SymptomId = 16, Weight = 10 },
                new DiseaseSymptom { DiseaseId = 6, SymptomId = 17, Weight = 9 },

                // Cholera
                new DiseaseSymptom { DiseaseId = 8, SymptomId = 5, Weight = 10 },
                new DiseaseSymptom { DiseaseId = 8, SymptomId = 4, Weight = 8 },

                // Measles
                new DiseaseSymptom { DiseaseId = 9, SymptomId = 1, Weight = 7 },
                new DiseaseSymptom { DiseaseId = 9, SymptomId = 3, Weight = 10 },
                new DiseaseSymptom { DiseaseId = 9, SymptomId = 6, Weight = 7 },

                // Hepatitis B
                new DiseaseSymptom { DiseaseId = 13, SymptomId = 11, Weight = 10 },
                new DiseaseSymptom { DiseaseId = 13, SymptomId = 12, Weight = 7 },
                new DiseaseSymptom { DiseaseId = 13, SymptomId = 22, Weight = 6 }
            );

            // ===================================================
            // DISEASE ↔ LAB TEST MAPPINGS
            // ===================================================

            builder.Entity<DiseaseLabTest>().HasData(

                // Dengue
                new DiseaseLabTest { DiseaseId = 1, LabTestId = 1 },
                new DiseaseLabTest { DiseaseId = 1, LabTestId = 2 },
                new DiseaseLabTest { DiseaseId = 1, LabTestId = 3 },

                // Malaria
                new DiseaseLabTest { DiseaseId = 2, LabTestId = 4 },
                new DiseaseLabTest { DiseaseId = 2, LabTestId = 5 },
                new DiseaseLabTest { DiseaseId = 2, LabTestId = 1 },

                // COVID
                new DiseaseLabTest { DiseaseId = 4, LabTestId = 6 },
                new DiseaseLabTest { DiseaseId = 4, LabTestId = 7 },

                // TB
                new DiseaseLabTest { DiseaseId = 6, LabTestId = 10 },
                new DiseaseLabTest { DiseaseId = 6, LabTestId = 11 },

                // Typhoid
                new DiseaseLabTest { DiseaseId = 7, LabTestId = 8 },
                new DiseaseLabTest { DiseaseId = 7, LabTestId = 9 },

                // Cholera
                new DiseaseLabTest { DiseaseId = 8, LabTestId = 16 },

                // Measles
                new DiseaseLabTest { DiseaseId = 9, LabTestId = 18 },

                // Rubella
                new DiseaseLabTest { DiseaseId = 10, LabTestId = 19 },

                // HIV
                new DiseaseLabTest { DiseaseId = 21, LabTestId = 20 }
            );

            // ===================================================
            // LAB TEST ↔ SAMPLE TYPE MAPPINGS
            // ===================================================

            builder.Entity<LabTestSampleType>().HasData(

                new LabTestSampleType { LabTestId = 1, SampleTypeId = 1 },
                new LabTestSampleType { LabTestId = 2, SampleTypeId = 2 },
                new LabTestSampleType { LabTestId = 3, SampleTypeId = 2 },
                new LabTestSampleType { LabTestId = 4, SampleTypeId = 1 },
                new LabTestSampleType { LabTestId = 5, SampleTypeId = 1 },
                new LabTestSampleType { LabTestId = 6, SampleTypeId = 5 },
                new LabTestSampleType { LabTestId = 7, SampleTypeId = 5 },
                new LabTestSampleType { LabTestId = 8, SampleTypeId = 1 },
                new LabTestSampleType { LabTestId = 9, SampleTypeId = 1 },
                new LabTestSampleType { LabTestId = 10, SampleTypeId = 7 },
                new LabTestSampleType { LabTestId = 11, SampleTypeId = 7 },
                new LabTestSampleType { LabTestId = 12, SampleTypeId = 6 },
                new LabTestSampleType { LabTestId = 13, SampleTypeId = 2 },
                new LabTestSampleType { LabTestId = 14, SampleTypeId = 1 },
                new LabTestSampleType { LabTestId = 15, SampleTypeId = 1 },
                new LabTestSampleType { LabTestId = 16, SampleTypeId = 4 },
                new LabTestSampleType { LabTestId = 17, SampleTypeId = 3 },
                new LabTestSampleType { LabTestId = 18, SampleTypeId = 2 },
                new LabTestSampleType { LabTestId = 19, SampleTypeId = 2 },
                new LabTestSampleType { LabTestId = 20, SampleTypeId = 2 },
                new LabTestSampleType { LabTestId = 21, SampleTypeId = 2 },
                new LabTestSampleType { LabTestId = 22, SampleTypeId = 2 },
                new LabTestSampleType { LabTestId = 23, SampleTypeId = 2 },
                new LabTestSampleType { LabTestId = 24, SampleTypeId = 2 },
                new LabTestSampleType { LabTestId = 25, SampleTypeId = 2 }
            );
        }
    }

}
