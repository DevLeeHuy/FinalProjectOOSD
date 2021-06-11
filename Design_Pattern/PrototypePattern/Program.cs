using System;

namespace PrototypePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup profile.
            Profile profile = new Profile()
            {
                Name = "PrototypeUser",
                Email = "AppDatBan@gmail.com"
            };
            profile.SetDateOfBirth(new DateTime(2000, 1, 1));
            profile.SetSettings(new ProfileSettings()
            {
                HideEmail = true,
                HideAge = false
            });

            DisplayProfile(profile);

            Console.ReadKey();

            // Edit profile.
            Console.Write("\n\n\n");
            Console.WriteLine("Edit Profile");
            Console.WriteLine("------------");

            Profile editProfile = profile.Clone();

            Console.Write("Name: ");
            editProfile.Name = Console.ReadLine();

            Console.Write("Email: ");
            editProfile.Email = Console.ReadLine();

            Console.Write("Private Profile (1 = yes): ");
            editProfile.IsPrivate = Console.ReadLine() == "1";

            Console.Write("Hide Email (1 = yes): ");
            editProfile.HideEmail = Console.ReadLine() == "1";

            Console.Write("Hide Age (1 = yes): ");
            editProfile.HideAge = Console.ReadLine() == "1";

            Console.Write("\n\n\n");
            DisplayProfile(editProfile);

            Console.WriteLine();
            Console.Write("Would you like to save these changes? (1 = yes)");
            bool saveChanges = Console.ReadLine() == "1";

            Console.WriteLine();

            if (saveChanges)
            {
                profile = editProfile;
                Console.WriteLine("Successfully saved profile.");
            }
            else
            {
                Console.WriteLine("Undoing profile changes...");
            }

            Console.Write("\n\n\n");

            DisplayProfile(profile);

            Console.ReadKey();

            // Create profile from prototype.
            ProfileSettings defaultSettingsPrototype = new ProfileSettings()
            {
                IsPrivate = false,
                HideAge = true,
                HideEmail = true
            };
            ProfileSettings secureSettingsPrototype = new ProfileSettings()
            {
                IsPrivate = true,
                HideAge = true,
                HideEmail = true
            };
            ProfileSettingsPrototypeRegistry settingsPrototypeRegistry = new ProfileSettingsPrototypeRegistry(
                defaultSettingsPrototype, secureSettingsPrototype);

            Console.Write("\n\n\n");
            Console.WriteLine("Create Profile");
            Console.WriteLine("------------");

            Profile newProfile = new Profile();

            Console.Write("Name: ");
            newProfile.Name = Console.ReadLine();

            Console.Write("Email: ");
            newProfile.Email = Console.ReadLine();

            Console.Write("Settings (1 = default, 2 = secure, other = custom): ");
            switch (Console.ReadLine())
            {
                case "1":
                    // Get default settings prototype.
                    newProfile.SetSettings(settingsPrototypeRegistry.CreateDefaultProfileSettings());
                    break;
                case "2":
                    // Get secure settings prototype.
                    newProfile.SetSettings(settingsPrototypeRegistry.CreateSecureProfileSettings());
                    break;
                default:
                    Console.Write("Private Profile (1 = yes): ");
                    newProfile.IsPrivate = Console.ReadLine() == "1";

                    Console.Write("Hide Email (1 = yes): ");
                    newProfile.HideEmail = Console.ReadLine() == "1";

                    Console.Write("Hide Age (1 = yes): ");
                    newProfile.HideAge = Console.ReadLine() == "1";
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Successfully created profile.");
            Console.Write("\n\n\n");

            DisplayProfile(newProfile);

            Console.ReadKey();
        }

        private static void DisplayProfile(Profile profile)
        {
            Console.WriteLine("General");
            Console.WriteLine("------------");
            Console.WriteLine($"Name: {profile.Name}");

            if (!profile.HideEmail)
            {
                Console.WriteLine($"Email: {profile.Email}");
            }

            if (!profile.HideAge)
            {
                Console.WriteLine($"Age: {profile.Age}");
            }

            Console.WriteLine();

            Console.WriteLine("Settings");
            Console.WriteLine("------------");
            Console.WriteLine($"Private: {profile.IsPrivate}");
            Console.WriteLine($"Hide Email: {profile.HideEmail}");
            Console.WriteLine($"Hide Age: {profile.HideAge}");
        }
        public class Profile
        {
            private DateTime _dateOfBirth;
            private ProfileSettings _settings;

            public string Name { get; set; }
            public string Email { get; set; }
            public int Age => CalculateAge();

            public bool IsPrivate
            {
                get => _settings.IsPrivate;
                set => _settings.IsPrivate = value;
            }

            public bool HideEmail
            {
                get => _settings.HideEmail;
                set => _settings.HideEmail = value;
            }

            public bool HideAge
            {
                get => _settings.HideAge;
                set => _settings.HideAge = value;
            }

            public Profile()
            {
                _settings = new ProfileSettings();
            }

            public void SetDateOfBirth(DateTime dateOfBirth)
            {
                _dateOfBirth = dateOfBirth;
            }

            public void SetSettings(ProfileSettings settings)
            {
                _settings = settings;
            }

            private int CalculateAge()
            {
                double ageInDays = TimeSpan.FromTicks(DateTime.Now.Ticks).TotalDays - TimeSpan.FromTicks(_dateOfBirth.Ticks).TotalDays;
                return (int)ageInDays / 365;
            }

            public Profile Clone()
            {
                return new Profile()
                {
                    Email = Email,
                    Name = Name,
                    _dateOfBirth = _dateOfBirth,
                    _settings = _settings.Clone()
                };
            }
        }

        public class ProfileSettings
        {
            public bool IsPrivate { get; set; }
            public bool HideEmail { get; set; }
            public bool HideAge { get; set; }

            public ProfileSettings Clone()
            {
                return new ProfileSettings()
                {
                    IsPrivate = IsPrivate,
                    HideEmail = HideEmail,
                    HideAge = HideAge
                };
            }
        }

        public class ProfileSettingsPrototypeRegistry
        {
            private readonly ProfileSettings _defaultProfileSettings;
            private readonly ProfileSettings _secureProfileSettings;

            public ProfileSettingsPrototypeRegistry(ProfileSettings defaultProfileSettings, ProfileSettings secureProfileSettings)
            {
                _defaultProfileSettings = defaultProfileSettings;
                _secureProfileSettings = secureProfileSettings;
            }

            public ProfileSettings CreateDefaultProfileSettings()
            {
                return _defaultProfileSettings.Clone();
            }

            public ProfileSettings CreateSecureProfileSettings()
            {
                return _secureProfileSettings.Clone();
            }
        }
    }
}
