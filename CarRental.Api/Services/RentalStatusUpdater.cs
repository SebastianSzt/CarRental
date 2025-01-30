using CarRental.Repository.Rentals;

namespace CarRental.Api.Services
{
    public class RentalStatusUpdater : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;

        public RentalStatusUpdater(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(UpdateRentalStatuses, null, TimeSpan.Zero, TimeSpan.FromHours(1));
            return Task.CompletedTask;
        }

        private async void UpdateRentalStatuses(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var rentalRepository = scope.ServiceProvider.GetRequiredService<IRentalRepository>();

                var rentals = await rentalRepository.GetAllRentalsAsync();
                var pendingRentals = rentals.Where(r => r.Status == "Pending" && r.EndDate < DateTime.Now).ToList();

                foreach (var rental in pendingRentals)
                {
                    rental.Status = "Completed";
                    await rentalRepository.SaveRentalAsync(rental);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
