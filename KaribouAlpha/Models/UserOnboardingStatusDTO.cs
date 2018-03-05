using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaribouAlpha.Models
{
    public class UserOnboardingStatusDTO
    {
        public Boolean OnboardingSkipped { get; set; }
        public int? OnboardingStep { get; set; }
        public bool? IsIndividual { get; set; }
      
    }
}