using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Data
{
    public class Speaker : ConferenceDTO.Speaker
    {
        public virtual ICollection<SessionSpeaker> SessionSpeakers { get; set; } = new List<SessionSpeaker>();
    }
}
