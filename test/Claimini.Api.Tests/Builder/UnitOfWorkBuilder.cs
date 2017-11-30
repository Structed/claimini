using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claimini.Api.Data;
using Claimini.Api.Repository;
using Moq;

namespace Claimini.Api.Tests.Builder
{
    public class UnitOfWorkBuilder
    {
        private readonly MockRepository mockRepository;
        private readonly Mock<IUnitOfWork> mock;

        public UnitOfWorkBuilder(MockRepository mockRepository)
        {
            this.mockRepository = mockRepository;
            this.mock = mockRepository.Create<IUnitOfWork>();
        }

        public UnitOfWorkBuilder WithCommitAffectingRows(int affectedRows)
        {
            this.mock.Setup(x => x.Commit()).Returns(affectedRows);

            return this;
        }

        public Mock<IUnitOfWork> Build()
        {
            return this.mock;
        }
    }
}
