using BuildingLink_Driver.Models;
using System.Data.SQLite;

namespace BuildingLink_Driver.Data;

public class DriversRepository : IDriversRepository
{
    private readonly SQLiteConnection _connection;

    public DriversRepository(SQLiteConnection connection)
    {
        _connection = connection;
    }

    /// <summary>
    /// Gets a list of all drivers.
    /// </summary>
    /// <returns>A list of drivers.</returns>
    public List<Driver> Get()
    {
        List<Driver> drivers = new();

        using SQLiteConnection connection = new(_connection);

        connection.Open();

        using SQLiteCommand command = new("SELECT * FROM drivers", connection);
        using SQLiteDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            drivers.Add(new Driver
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Email = reader.GetString(3),
                PhoneNumber = reader.GetString(4)
            });
        }

        return drivers;
    }

    /// <summary>
    /// Gets a driver by id.
    /// </summary>
    /// <param name="id">The id of the driver to get.</param>
    /// <returns>A driver or null if the driver does not exist.</returns>
    public Driver? Get(int id)
    {
        Driver? driver = null;

        using SQLiteConnection connection = new(_connection);

        connection.Open();

        using SQLiteCommand command = new("SELECT * FROM drivers WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", id);
        using SQLiteDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            driver = new Driver
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Email = reader.GetString(3),
                PhoneNumber = reader.GetString(4)
            };
        }

        return driver;
    }

    /// <summary>
    /// Adds a new driver to the database.
    /// </summary>
    /// <param name="driver">The driver to add.</param>
    ///<returns>The number of rows affected by the insert.</returns>
    public int Add(Driver driver)
    {
        using SQLiteConnection connection = new(_connection);

        connection.Open();

        using SQLiteCommand command = new("INSERT OR IGNORE INTO drivers (firstName, lastName, email, phoneNumber) VALUES (@firstName, @lastName, @email, @phoneNumber)", connection);
        command.Parameters.AddWithValue("@firstName", driver.FirstName);
        command.Parameters.AddWithValue("@lastName", driver.LastName);
        command.Parameters.AddWithValue("@email", driver.Email);
        command.Parameters.AddWithValue("@phoneNumber", driver.PhoneNumber);
        return command.ExecuteNonQuery();
    }

    /// <summary>
    /// Updates an existing driver in the database.
    /// </summary>
    /// <param name="driver">The driver to update.</param>
    /// <returns>The number of rows affected by the update.</returns>
    public int Update(Driver driver)
    {
        using SQLiteConnection connection = new(_connection);
        connection.Open();

        using SQLiteCommand command = new("UPDATE drivers SET firstName = @firstName, lastName = @lastName, email = @email, phoneNumber = @phoneNumber WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", driver.Id);
        command.Parameters.AddWithValue("@firstName", driver.FirstName);
        command.Parameters.AddWithValue("@lastName", driver.LastName);
        command.Parameters.AddWithValue("@email", driver.Email);
        command.Parameters.AddWithValue("@phoneNumber", driver.PhoneNumber);
        return command.ExecuteNonQuery();
    }

    /// <summary>
    /// Deletes a driver by id.
    /// </summary>
    /// <param name="id">The id of the driver to delete.</param>
    /// <returns>The number of rows affected by the delete.</returns>
    public int Delete(int id)
    {
        using SQLiteConnection connection = new(_connection);

        connection.Open();

        using SQLiteCommand command = new("DELETE FROM drivers WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", id);
        return command.ExecuteNonQuery();
    }

    /// <summary>
    /// Bulk inserts drivers into the database.
    /// </summary>
    /// <param name="drivers">The drivers to insert.</param>
    public void BulkInsert(IEnumerable<Driver> drivers)
    {
        using SQLiteConnection connection = new(_connection);
        connection.Open();

        using var transaction = connection.BeginTransaction();

        using SQLiteCommand command = new("INSERT OR IGNORE INTO drivers (firstName, lastName, Email, PhoneNumber) VALUES (@firstName, @lastName, @Email, @PhoneNumber)", connection);

        foreach (Driver driver in drivers)
        {
            command.Parameters.AddWithValue("@firstName", driver.FirstName);
            command.Parameters.AddWithValue("@lastName", driver.LastName);
            command.Parameters.AddWithValue("@Email", driver.Email);
            command.Parameters.AddWithValue("@PhoneNumber", driver.PhoneNumber);
            command.ExecuteNonQuery();
        }

        transaction.Commit();
    }
}