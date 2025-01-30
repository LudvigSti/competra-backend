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
                Username = "John",
                Password = "$2b$10$jne5qzW/fuDlZrmoNd9HA.eX61UUaVP4A2voVWLDwauZ5FiW437Qm"
            };

            User user2 = new User
            {
                Id = 2,
                Username = "Ibz",
                Password = "$2b$10$jne5qzW/fuDlZrmoNd9HA.eX61UUaVP4A2voVWLDwauZ5FiW437Qm"
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

        }
        
        public List<User> Users { get { return _users; } }
        public List<Group> GroupList { get { return _groups; } }
        public List<Activity> ActivityList { get { return _activities; } }
        public List<UserActivity> UserActivityList { get { return _userActivities; } }
        public List<UserGroup> UserGroupList { get { return _userGroups; } }
        public List<Match> MatchList {  get { return _matches; } }
        
    }
}
