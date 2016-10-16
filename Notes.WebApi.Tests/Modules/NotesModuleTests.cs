using Notes.Contract.Commands;
using Notes.Contract.Model;
using Notes.Contract.Queries;
using Notes.Core.Infrastructure.Extensions;
using Notes.WebApi.Tests.Fakes;
using Notes.WebApi.Tests.Infrastructure;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.WebApi.Tests.Modules
{
    [TestFixture]
    public class NotesModuleTests: NancyModuleTestsBase
    {
        private Core.Model.Note[] _notes;

        [SetUp]
        public async Task TestsSetup()
        {
            _notes = new[] { new Core.Model.Note { Id = Guid.NewGuid(), Body = "body", Title = "title", OwnerId = AccountRepositoryFake.TestAccount.Id } };

            NotesRepositoryFake.Instance.Notes.AddRange(_notes);
            await LoginTest();
        }

        [Test]
        public async Task GetNotes_ReturnsNotes()
        {
            //Act
            var result = await Get<GetNotesQueryResult>("notes");

            Assert.NotNull(result);
            Assert.AreEqual(_notes.Length, result.Notes.Count());
            Assert.AreEqual(_notes.First().Title, result.Notes.First().Title);
            Assert.AreEqual(_notes.First().Body, result.Notes.First().Body);
            Assert.AreEqual(_notes.First().Id, result.Notes.First().Id);
        }

        [Test]
        public async Task SetTitle_UpdatesTitle()
        {
            var newTitle = "new title";
            //Act
            await Put("notes/title", new SetNoteTitleCommand() { NoteId = _notes[0].Id, Title = newTitle });

            Assert.AreEqual(newTitle, _notes[0].Title);
        }

        [Test]
        public async Task SetBody_UpdatesBody()
        {
            var newBody = "new body";
            //Act
            await Put("notes/body", new SetNoteBodyCommand() { NoteId = _notes[0].Id, Body = newBody });

            Assert.AreEqual(newBody, _notes[0].Body);
        }

        [Test]
        public async Task PutNote_UpdatesNote()
        {
            var newBody = "new body";
            var newTitle = "new title";
            //Act
            await Put("notes", new SaveNoteCommand() { Note = new Note() { Id = _notes[0].Id, Body = newBody, Title = newTitle } });

            Assert.AreEqual(newBody, _notes[0].Body);
            Assert.AreEqual(newTitle, _notes[0].Title);
        }

        [Test]
        public async Task DeleteNote_DeletesNote()
        {
            var noteId = _notes[0].Id;
            //Act
            await Delete($"notes/{noteId}");

            Assert.IsFalse(NotesRepositoryFake.Instance.Notes.Any(x => x.Id == noteId));
        }
    }
}
