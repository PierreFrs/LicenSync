using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Entities;

namespace Core.Specifications;

public class ArtistSpecification(Guid trackId) : BaseSpecification<Artist>(a => a.TrackContributions.Any(tac => tac.TrackId == trackId));
