namespace Jet_API1.Model.Base
{
    public class BaseModel
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
