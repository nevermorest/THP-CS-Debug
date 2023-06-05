// Главная форма приложения
public class MainForm : Form
{
    private ListBox weaponsListBox;
    private Button addButton;
    private Button deleteButton;
    private Button editButton;
    private Label totalCostLabel;

    private WeaponsCalculator weaponsCalculator;
    private List<Weapon> weapons;

    public MainForm()
    {
        InitializeComponent();

        // Создание объектов предметной области с использованием фабричного метода
        weapons = new List<Weapon>();
        weapons.Add(WeaponFactory.CreateFiregun("AK-47", 1000, 2.5f, 30, "7.62mm"));
        weapons.Add(WeaponFactory.CreateBlades("Machete", 200, 1.0f, 12.0f, 1));

        // Инициализация логики приложения
        weaponsCalculator = new WeaponsCalculator();

        // Инициализация списка вооружения
        UpdateWeaponsListBox();

        // Обновление общей стоимости
        UpdateTotalCost();
    }

    private void InitializeComponent()
    {
        // Инициализация компонентов формы
        // ...

        // Добавление обработчиков событий
        addButton.Click += AddButton_Click;
        deleteButton.Click += DeleteButton_Click;
        editButton.Click += EditButton_Click;
        weaponsListBox.SelectedIndexChanged += WeaponsListBox_SelectedIndexChanged;
    }

    private void AddButton_Click(object sender, EventArgs e)
    {
        AddEditForm addEditForm = new AddEditForm();

        if (addEditForm.ShowDialog() == DialogResult.OK)
        {
            Weapon weapon = addEditForm.Weapon;
            weapons.Add(weapon);
            weaponsListBox.Items.Add(weapon);
            UpdateTotalCost();
        }
    }

    private void DeleteButton_Click(object sender, EventArgs e)
    {
        if (weaponsListBox.SelectedItem != null)
        {
            Weapon selectedWeapon = (Weapon)weaponsListBox.SelectedItem;
            weapons.Remove(selectedWeapon);
            weaponsListBox.Items.Remove(selectedWeapon);
            UpdateTotalCost();
        }
    }

    private void EditButton_Click(object sender, EventArgs e)
    {
        if (weaponsListBox.SelectedItem != null)
        {
            Weapon selectedWeapon = (Weapon)weaponsListBox.SelectedItem;
            AddEditForm addEditForm = new AddEditForm(selectedWeapon);

            if (addEditForm.ShowDialog() == DialogResult.OK)
            {
                // Обновление атрибутов объекта
                selectedWeapon.Name = addEditForm.Weapon.Name;
                // ...

                UpdateWeaponsListBox();
                UpdateTotalCost();
            }
        }
    }

    private void WeaponsListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (weaponsListBox.SelectedItem != null)
        {
            editButton.Enabled = true;
        }
        else
        {
            editButton.Enabled = false;
        }
    }

    private void UpdateWeaponsListBox()
    {
        weaponsListBox.Items.Clear();
        weaponsListBox.Items.AddRange(weapons.ToArray());
    }

    private void UpdateTotalCost()
    {
        decimal totalCost = weaponsCalculator.CalculateTotalCost(weapons);
        totalCostLabel.Text = "Total cost: $" + totalCost;
    }
}

// Форма добавления/редактирования объектов
public class AddEditForm : Form
{
    private TextBox nameTextBox;
    // ...

    private Weapon weapon;

    public AddEditForm()
    {
        InitializeComponent();
        weapon = null;
    }