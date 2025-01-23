using competra.wwwapi.Models;

namespace competra.wwwapi.Data
{
    public class Seeder
    {

        private List<Group> _groups = new List<Group>();
        private List<User> _users = new List<User>();
        private List<Activity> _activities = new List<Activity>();
        private List<UserActivity> _userActivities = new List<UserActivity>();
        private List<UserGroup> _userGroups = new List<UserGroup>();
        private List<Match> _matches = new List<Match>();

        public Seeder()
        {

            // Create Users
            User user1 = new User
            {
                Id = 1,
                FirstName = "John",
                LastName = "Cena",
                DateOfBirth = new DateOnly(2000, 5, 15),
                Email = "john.CEna@example.com",
                Password = "password123"
            };

            User user2 = new User
            {
                Id = 2,
                FirstName = "Ibz",
                LastName = "DaGOat",
                DateOfBirth = new DateOnly(1998, 8, 20),
                Email = "ibzinho@example.com",
                Password = "password123"
            };

            _users.Add(user1);
            _users.Add(user2);

            // Create Group
            Group group = new Group
            {
                Id = 1,
                GroupName = "Experis_aca",
                LogoUrl = "",
                CreatedAt = new DateOnly(2023, 1, 1)
            };

            _groups.Add(group);

            // Create Activity
            Activity activity = new Activity
            {
                Id = 1,
                ActivityName = "Bordtennis",
                GroupId = group.Id
            };

            _activities.Add(activity);

            // Link Users to Group (UserGroup)
            UserGroup userGroup1 = new UserGroup
            {
                UserId = user1.Id,
                GroupId = group.Id
            };

            UserGroup userGroup2 = new UserGroup
            {
                UserId = user2.Id,
                GroupId = group.Id
            };

            _userGroups.Add(userGroup1);
            _userGroups.Add(userGroup2);

            // Create UserActivities (Elo ratings for each user in the activity)
            UserActivity userActivity1 = new UserActivity
            {
                Id = 1,
                UserId = user1.Id,
                Elo = 1500,
                ActivityId = activity.Id
            };

            UserActivity userActivity2 = new UserActivity
            {
                Id = 2,
                UserId = user2.Id,
                Elo = 1500,
                ActivityId = activity.Id
            };

            _userActivities.Add(userActivity1);
            _userActivities.Add(userActivity2);

            // Create a Match between the two users in the activity
            Match match = new Match
            {
                Id = 1,
                P1Id = user1.Id,
                P2Id = user2.Id,
                CreatedAt = new DateOnly(2023, 7, 15),
                ActivityId = activity.Id,
                EloChangeP1 = 10,
                EloChangeP2 = -10,
                P1Result = 1,  // User 1 won
                P2Result = 0,  // User 2 lost
            };

            _matches.Add(match);
        }

        public List<User> Users { get { return _users; } }
        public List<Group> GroupList { get { return _groups; } }
        public List<Activity> ActivityList { get { return _activities; } }
        public List<UserActivity> UserActivityList { get { return _userActivities; } }
        public List<UserGroup> UserGroupList { get { return _userGroups; } }
        public List<Match> MatchList {  get { return _matches; } }

    }
}
