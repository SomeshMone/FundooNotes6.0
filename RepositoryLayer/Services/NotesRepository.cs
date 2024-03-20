using CommonLayer.Model;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using static System.Net.Mime.MediaTypeNames;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace RepositoryLayer.Services
{
    public class NotesRepository:INotesRepository
    {
        private readonly FundooContext fundooContext;
        public NotesRepository(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public string CreateNote(NotesModel request,long userid)
        {
            if (userid != 0)
            {

               
                UserEntity user = fundooContext.UsersTable1.FirstOrDefault(x=>x.UserId== userid);
                if (user != null)
                {
                    NotesEntity note = new NotesEntity();
                    note.Title = request.Title;
                    note.Description = request.Description;
                    note.Color = request.Color;
                    note.Remainder = request.Reminder;
                    note.IsArchive = request.IsArchive;
                    note.IsPinned = request.IsPinned;
                    note.IsTrash = request.IsTrash;
                    note.CreatedAt = request.CreatedAt;
                    note.ModifiedAt = request.ModifiedAt;
                    note.UserId = userid;


                    fundooContext.Add(note);
                    fundooContext.SaveChanges();
                    return "Note Created Sucessfully";

                }
            }
            else
            {
                return null;
            }

            return null;
            
        }



        public bool UpdateNote(long userid,long noteid,NotesModel model)
        {
            var res = fundooContext.UserNotes.FirstOrDefault(x => x.UserId == userid && x.NoteId == noteid);
            if (res!=null)
            {
                res.Title = model.Title;
                res.Description = model.Description;
                res.Color = model.Color;
                res.Remainder = model.Reminder;
                res.IsArchive = model.IsArchive;
                res.IsPinned = model.IsPinned;
                res.IsTrash = model.IsTrash;
                res.CreatedAt = model.CreatedAt;
                res.ModifiedAt = model.ModifiedAt;
                res.UserId = userid;
                fundooContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteNote(long userid,long noteid)
        {
            var note=fundooContext.UserNotes.FirstOrDefault(x=>x.UserId==userid && x.NoteId==noteid);
            if (note != null)
            {
                fundooContext.Remove(note); 
                fundooContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public IEnumerable<NotesEntity> GetAllNotes()
        {
            var notes = fundooContext.UserNotes.ToList();
            if(notes.Count==0 || notes == null)
            {
                return null;
            }
            else
            {
                return notes.ToList();
            }
        }

        public NotesEntity GetNoteById(long userid,long noteid)
        {
            var note=fundooContext.UserNotes.FirstOrDefault(x=>x.UserId==userid&&x.NoteId==noteid);
            if (note != null)
            {
                return note;
            }
            else
            {
                return null;
            }
        }
        public IEnumerable<NotesEntity> GetAllNotesByCache()
        {
            var notes = fundooContext.UserNotes.ToList();
            if (notes.Count == 0 || notes == null)
            {
                return null;
            }
            else
            {
                return notes.ToList();
            }
        }

        public IEnumerable<NotesEntity> GetNotesByUserId(long userid)
        {
            var notes = fundooContext.UserNotes.Where(x => x.UserId == userid).ToList();
            if (notes== null)
            {
                return null;
            }
            else
            {
                return notes;
            }
        
        }

        public NotesEntity TogglePin(long userid,long noteid)
        {
            var pin = fundooContext.UserNotes.FirstOrDefault(x => x.UserId == userid && x.NoteId == noteid);
            if (pin != null)
            {
                if (pin.IsPinned == true)
                {
                    pin.IsPinned = false;
                    fundooContext.Entry(pin).State = EntityState.Modified;
                    fundooContext.SaveChanges();
                    return pin;
                }
                else
                {
                    pin.IsPinned = true;
                    fundooContext.Entry(pin).State = EntityState.Modified;
                    fundooContext.SaveChanges();
                    return pin;
                }
            }
            else
            {
                return null;
            }
        }

        public NotesEntity ToggleArchive(long userid,long noteid)
        {
            var archive = fundooContext.UserNotes.FirstOrDefault(x => x.UserId == userid && x.NoteId == noteid);
            if (archive == null)
            {
                return null;
            }
            else
            {
                if (archive.IsArchive == true)
                {
                    archive.IsArchive = false;
                    if(archive.IsPinned== true)
                    {
                        archive.IsPinned = false;
                    }
                    if(archive.IsTrash == true)
                    {
                        archive.IsTrash = false;
                    }
                    fundooContext.Entry(archive).State=EntityState.Modified; 
                    fundooContext.SaveChanges();
                    return archive;

                }
                else
                {
                    archive.IsArchive = true;
                    if(archive.IsPinned == true)
                    {
                        archive.IsPinned = false;
                    }
                    if (archive.IsTrash == true)
                    {
                        archive.IsTrash = false;
                    }
                }
                fundooContext.Entry(archive).State = EntityState.Modified;
                fundooContext.SaveChanges();
                return archive;

            }
            
        }


        public NotesEntity ToggleTrash(long userid,long noteid)
        {
            var trash = fundooContext.UserNotes.FirstOrDefault(x => x.NoteId == noteid && x.UserId == userid);
            if (trash == null)
            {
                return null;
            }
            else
            {
                if(trash.IsTrash == true)
                {
                    trash.IsTrash = false;
                    if(trash.IsPinned == true)
                    {
                        trash.IsPinned = false;
                    }
                    if(trash.IsArchive== true)
                    {
                        trash.IsArchive = false;
                    }
                    fundooContext.Entry(trash).State = EntityState.Modified;
                    fundooContext.SaveChanges();
                }
                else
                {
                    trash.IsTrash = true;
                    if (trash.IsPinned == true)
                    {
                        trash.IsPinned = false;
                    }
                    if (trash.IsArchive == true)
                    {
                        trash.IsArchive = false;
                    }
                    fundooContext.Entry(trash).State = EntityState.Modified;
                    fundooContext.SaveChanges();
                }

                return trash;
            }
            
        }
        public NotesEntity AddReminder(long userid,long noteid,DateTime reminder)
        {
            var note = fundooContext.UserNotes.FirstOrDefault(x => x.NoteId == noteid && x.UserId == userid);
            if (note == null)
            {
                return null;
            }
            else
            {
                if (reminder > DateTime.Now)
                {
                    note.Remainder = reminder;
                    fundooContext.Entry(note).State = EntityState.Modified;
                    fundooContext.SaveChanges();
                    return note;

                }
                else
                {
                    return null;
                }
            }
        }
        public NotesEntity AddColor(long userid,long noteid,string color)
        {
            var note = fundooContext.UserNotes.FirstOrDefault(x => x.NoteId == noteid && x.UserId == userid);
            if (note != null)
            {
                note.Color = color;
                fundooContext.Entry(note).State = EntityState.Modified;
                fundooContext.SaveChanges();
                return note;
            }
            else
            {
                return null;
            }
        }
        public int GetNofNotes(long userid)
        {
            var notes=fundooContext.UserNotes.Where(x=>x.UserId == userid).Count();
            if (notes != 0)
            {
                return notes;
            }
            else
            {
                return 0;
            }
        }

        public NotesEntity GetNoteTitle(long userid,string title,string desc) 
        {
            var notes = fundooContext.UserNotes.FirstOrDefault(x => x.UserId == userid && x.Title.Contains(title)&&x.Description.Contains(desc));
                if (notes != null)
                {
                    return notes;
                }
                else
                {
                    return null;
                }
            
        }
        public NotesEntity AddImage(long userId, long noteId, IFormFile Image)
        {
            try
            {


                var note = fundooContext.UserNotes.Where(x => x.UserId == userId && x.NoteId == noteId).FirstOrDefault();

                if (note == null)
                {
                    return null;
                }
                else
                {
                    Account account = new Account(
                      "dt5g4ga4t",
                      "734587615747541",
                      "zmVE40OjSubH4kPblEKlpyIvWf4");

                    Cloudinary cloudinary = new Cloudinary(account);


                    var uploadParameters = new ImageUploadParams()
                    {
                        File = new FileDescription(Image.FileName, Image.OpenReadStream()),
                        PublicId=note.Title
                    };
                    var uploadResult = cloudinary.Upload(uploadParameters);
                    string ImagePath = uploadResult.Url.ToString();


                    note.Image = ImagePath;
                    fundooContext.Entry(note).State = EntityState.Modified;
                    fundooContext.SaveChanges();

                    return note;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        

    }
}
