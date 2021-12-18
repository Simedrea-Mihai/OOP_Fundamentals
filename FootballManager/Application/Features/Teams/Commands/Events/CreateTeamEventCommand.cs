using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Features.Managers.Commands.Create;
using Application.Features.Players.Commands.CreateMultiple;
using Application.Features.Teams.Commands.AddManager;
using Domain;
using MediatR;

namespace Application.Teams.CreateTeam
{
    public class CreateTeamEventCommand : IRequest<Team>
    {
        

    }

    public class CreateTeamEventCommandHandler : IRequestHandler<CreateTeamEventCommand, Team>
    {
        private readonly ITeamRepository _repository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly Random rnd = new Random();

        public CreateTeamEventCommandHandler(ITeamRepository repository, IProfileRepository profileRepository, IPlayerRepository playerRepository, IManagerRepository managerRepository)
        {
            _repository = repository;
            _playerRepository = playerRepository;
            _profileRepository = profileRepository;
            _managerRepository = managerRepository;
        }


        public async Task<Team> Handle(CreateTeamEventCommand command, CancellationToken cancellationToken)
        {
            CreateTeamCommand createTeamCommand = new CreateTeamCommand();
            CreateTeamHandler createTeamCommandHandler = new CreateTeamHandler(_repository);
            var team = await createTeamCommandHandler.Handle(createTeamCommand, cancellationToken);
            team.Name = "team" + team.Id.ToString();
            team.Description = "Some description for the team with id : " + team.Id.ToString();
            team.HeaderDescription = "Some header description for the team with id : " + team.Id.ToString();

            CreatePlayersCommand playersCommand = new CreatePlayersCommand();
            playersCommand.Count = rnd.Next(15, 25);

            CreatePlayersCommandHandler playersCommandHandler = new CreatePlayersCommandHandler(_playerRepository, _profileRepository);

            await playersCommandHandler.Handle(playersCommand, cancellationToken);

            AddPlayers.AddPlayers addPlayersCommand = new AddPlayers.AddPlayers();
            addPlayersCommand.TeamId = team.Id;
            addPlayersCommand.PlayersCount = playersCommand.Count;

            AddPlayers.AddPlayersHandler addPlayersHandler = new AddPlayers.AddPlayersHandler(_playerRepository, _profileRepository, _repository);

            await addPlayersHandler.Handle(addPlayersCommand, cancellationToken);


            CreateRandomManagerCommand createRandomManagerCommand = new CreateRandomManagerCommand();
            CreateRandomManagerCommandHandler createRandomManagerCommandHandler = new CreateRandomManagerCommandHandler(_managerRepository, _profileRepository);

            var manager = await createRandomManagerCommandHandler.Handle(createRandomManagerCommand, cancellationToken);

            AddManager addManagerCommand = new AddManager();

            addManagerCommand.TeamId = team.Id;
            addManagerCommand.ManagerId = manager.Id;

            AddManagerHandler addManagerHandler = new AddManagerHandler(_managerRepository, _repository);
            await addManagerHandler .Handle(addManagerCommand, cancellationToken);

            return team;
        }
    }


}
