using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using RepositoryLayer.Migrations;

namespace RepositoryLayer.Entity
{
    public class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LabelId { get; set; }
        public string LabelName {  get; set; }

        [ForeignKey("UsersTable1")]
        public long UserId {  get; set; }

        [JsonIgnore]
        public virtual UserEntity UsersTable1 {  get; set; }

        [ForeignKey("UserNotes")]
        public long NoteId { get; set; }

        [JsonIgnore]
        public virtual NotesEntity UserNotes { get; set; }

    }
}
