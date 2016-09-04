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
        private Core.Model.Note[] notes;

        [SetUp]
        public void TestsSetup()
        {
            notes = new[] { new Core.Model.Note { Id = Guid.NewGuid(), Body = "body", Title = "title" } };

            NotesRepositoryFake.Instance.Notes.AddRange(notes);
        }

        [Test]
        public async Task GetNotes_ReturnsNotes()
        {
            //Act
            var result = await Get<GetNotesQueryResult>("notes");

            Assert.NotNull(result);
            Assert.AreEqual(notes.Length, result.Notes.Count());
            Assert.AreEqual(notes.First().Title, result.Notes.First().Title);
            Assert.AreEqual(notes.First().Body, result.Notes.First().Body);
            Assert.AreEqual(notes.First().Id, result.Notes.First().Id);
        }

        [Test]
        public async Task SetTitle_UpdatesTitle()
        {
            var newTitle = "new title";
            //Act
           await Put("notes/title",new SetNoteTitleCommand() { NoteId = notes[0].Id, Title = newTitle });

            Assert.AreEqual(newTitle, notes[0].Title);
        }

    }
}
