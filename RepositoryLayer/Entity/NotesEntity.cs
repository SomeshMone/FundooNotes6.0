using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryLayer.Entity
{
    public class NotesEntity
    {
        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public DateTime Remainder { get; set; }
        public bool IsArchive { get; set; }
        public bool IsPinned { get; set; }
        public bool IsTrash { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        [ForeignKey("UsersTable1")]
        public long UserId { get; set; }

        [JsonIgnore]
        public virtual UserEntity UsersTable1 { get; set; }
        
    }
}
