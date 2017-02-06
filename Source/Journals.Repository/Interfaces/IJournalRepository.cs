using System.Collections.Generic;
using Medico.Model;

namespace Medico.Repository.Interfaces
{
    public interface IJournalRepository
    {
        List<Journal> GetAllJournals(int userId);

        OperationStatus AddJournal(Journal newJournal);

        Journal GetJournalById(int Id);

        OperationStatus DeleteJournal(Journal journal);

        OperationStatus UpdateJournal(Journal journal);
    }
}