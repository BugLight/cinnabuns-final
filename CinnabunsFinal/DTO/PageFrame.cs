using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinnabunsFinal.DTO
{
    public class PageFrame
    {
        public int Limit { get; set; } = 10;
        public int Offset { get; set; } = 0;
    }

    public class PageFrameValidator : AbstractValidator<PageFrame>
    {
        public PageFrameValidator()
        {
            RuleFor(x => x.Limit).NotNull().GreaterThan(0).LessThanOrEqualTo(100);
            RuleFor(x => x.Offset).NotNull().GreaterThanOrEqualTo(0);
        }
    }

    public class PageFrameDb<Model>
    {
        public IQueryable<Model> FrameDb(IQueryable<Model> q, PageFrame pageFrame)
        {
            return q.Skip(pageFrame.Offset).Take(pageFrame.Limit);
        }
    }
}
