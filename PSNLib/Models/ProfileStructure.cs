namespace PSNLib.Models
{
    public class PSProfile : PsnProfile
    {
       
    }

    public class FriendProfile : PsnProfile
    {
    }

    public class PsnProfile
    {
        public virtual string OnlineId
        {
            get; internal set;
        }
        public virtual string AvatarUrl
        {
            get; internal set;
        }
        public virtual string AboutMe
        {
            get; internal set;
        }
    }
}