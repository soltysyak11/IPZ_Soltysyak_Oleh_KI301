using System;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Npgsql;

namespace LAB2
{
    public partial class VideoMonitoringForm : Form
    {
        private VideoCapture _videoCapture;
        private bool _isCameraRunning = false;
        private readonly string _connectionString = "Host=localhost;Port=5432;Database=ipz;Username=postgres;Password=gamingf17;";

        public VideoMonitoringForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Set placeholder text for observationTextBox
            observationTextBox.Text = "Введіть новину";
            observationTextBox.ForeColor = System.Drawing.Color.Gray;
            observationTextBox.Enter += (s, e) =>
            {
                if (observationTextBox.Text == "Введіть новину")
                {
                    observationTextBox.Text = "";
                    observationTextBox.ForeColor = System.Drawing.Color.Black;
                }
            };
            observationTextBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(observationTextBox.Text))
                {
                    observationTextBox.Text = "Введіть новину";
                    observationTextBox.ForeColor = System.Drawing.Color.Gray;
                }
            };

            // Set placeholder text for importanceComboBox
            importanceComboBox.Text = "Виберіть важливість";
            importanceComboBox.ForeColor = System.Drawing.Color.Gray;
            importanceComboBox.Enter += (s, e) =>
            {
                if (importanceComboBox.Text == "Виберіть важливість")
                {
                    importanceComboBox.Text = "";
                    importanceComboBox.ForeColor = System.Drawing.Color.Black;
                }
            };
            importanceComboBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(importanceComboBox.Text))
                {
                    importanceComboBox.Text = "Виберіть важливість";
                    importanceComboBox.ForeColor = System.Drawing.Color.Gray;
                }
            };

            // Set placeholder text for recordedByTextBox
            recordedByTextBox.Text = "Автор";
            recordedByTextBox.ForeColor = System.Drawing.Color.Gray;
            recordedByTextBox.Enter += (s, e) =>
            {
                if (recordedByTextBox.Text == "Автор")
                {
                    recordedByTextBox.Text = "";
                    recordedByTextBox.ForeColor = System.Drawing.Color.Black;
                }
            };
            recordedByTextBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(recordedByTextBox.Text))
                {
                    recordedByTextBox.Text = "Автор";
                    recordedByTextBox.ForeColor = System.Drawing.Color.Gray;
                }
            };
        }

        private void StartMonitoringButton_Click(object sender, EventArgs e)
        {
            if (!_isCameraRunning)
            {
                try
                {
                    _videoCapture = new VideoCapture(0);

                    if (!_videoCapture.IsOpened)
                    {
                        MessageBox.Show("Не вдалося відкрити камеру. Спробуйте інший індекс.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    _videoCapture.ImageGrabbed += ProcessFrame;
                    _videoCapture.Start();
                    _isCameraRunning = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при запуску відео: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void StopMonitoringButton_Click(object sender, EventArgs e)
        {
            if (_isCameraRunning)
            {
                _videoCapture.Stop();
                _videoCapture.Dispose();
                _isCameraRunning = false;
                videoPictureBox.Image = null;
            }
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            try
            {
                Mat frame = new Mat();
                _videoCapture.Retrieve(frame);
                videoPictureBox.Image = frame.ToImage<Bgr, byte>().ToBitmap();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при обробці кадру: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveObservationButton_Click(object sender, EventArgs e)
        {
            string observationText = observationTextBox.Text;
            string importance = importanceComboBox.SelectedItem?.ToString();
            DateTime timestamp = DateTime.Now;
            string recordedBy = recordedByTextBox.Text;

            if (string.IsNullOrWhiteSpace(observationText) || string.IsNullOrWhiteSpace(importance) || string.IsNullOrWhiteSpace(recordedBy))
            {
                MessageBox.Show("Будь ласка, заповніть всі поля для запису спостереження.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Observations (Timestamp, RecordedBy, ObservationText, Importance) VALUES (@Timestamp, @RecordedBy, @ObservationText, @Importance)";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Timestamp", timestamp);
                        command.Parameters.AddWithValue("@RecordedBy", recordedBy);
                        command.Parameters.AddWithValue("@ObservationText", observationText);
                        command.Parameters.AddWithValue("@Importance", importance);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Спостереження успішно збережено в базі даних.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при збереженні спостереження: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_isCameraRunning)
            {
                _videoCapture.Stop();
                _videoCapture.Dispose();
            }

            base.OnFormClosing(e);
        }
    }
}
