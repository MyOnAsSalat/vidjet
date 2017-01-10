Imports System.Windows.Threading
Imports System.IO

Class MainWindow
    Dim date_dmy As Date = Now
    Dim USERNAME As String = "Юзер"
    Dim setting_button_press As Boolean = False
    Dim updater As New DispatcherTimer
    Dim usecpu As New PerformanceCounter
    Dim usememory As New PerformanceCounter
    Dim work_h As Integer = SystemParameters.WorkArea.Height
    Dim work_w As Integer = SystemParameters.WorkArea.Width
    Dim girl_config() As String
    Dim phrase As Integer = 2



    Private Sub updater_Tick(ByVal sender As Object, ByVal e As EventArgs)
        'поля обновления      
        clock.Content = Strings.Left(TimeString, 5)

        date_clock.Content = Format(date_dmy, " dd.MM.yy")
        Dim cpuprocent As Integer = usecpu.NextValue
        cpu.Content = cpuprocent & "%"
        Dim memoryprocent As Integer = usememory.NextValue
        memory.Content = memoryprocent & "%"
        'конец
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        USERNAME = namebox.Text
        Try
            IO.File.WriteAllText(Environ("TEMP") & "/vidjet.name", "")

            'запись в файл
            Using fstream As New FileStream(Environ("TEMP") & "/vidjet.name", FileMode.OpenOrCreate)
                'преобразуем строку в байты
                Dim array As Byte() = System.Text.Encoding.Default.GetBytes(USERNAME)
                'записываем массив байтов в файл
                fstream.Write(array, 0, array.Length)
            End Using
        Catch ex As Exception
        End Try


        Dim subnametext As String
        subnametext = "Привет, " & USERNAME & "."
        dbox.Content = subnametext
        namebox.Visibility = True
        namebutton.Visibility = True
        clockbutton.Visibility = False
        date_clock.Visibility = False
        clock.Visibility = False
        cpubutton.Visibility = False
        memorybutton.Visibility = False
        help.Visibility = False
        setting.Visibility = False
    End Sub
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        'расположение формы
        global_wpf_form.Left = work_w - 450
        global_wpf_form.Top = work_h - 524
        'конец

        Try
            'проверка существования файла
            If IO.File.Exists(Environ("TEMP") & "/vidjet.name") Then
                'чтение из файла
                Using fstream As FileStream = File.OpenRead(Environ("TEMP") & "/vidjet.name")
                    'преобразуем строку в байты
                    Dim array As Byte() = New Byte(fstream.Length) {}
                    'чтение данных
                    fstream.Read(array, 0, array.Length)
                    'декодируем байты в строку
                    USERNAME = System.Text.Encoding.Default.GetString(array)
                    namebox.Text = USERNAME
                End Using
                Using fstream1 As FileStream = File.OpenRead(Environ("TEMP") & "/vidjet.workh")
                    'преобразуем строку в байты
                    Dim array As Byte() = New Byte(fstream1.Length) {}
                    'чтение данных
                    fstream1.Read(array, 0, array.Length)
                    'декодируем байты в строку
                    work_h = Integer.Parse(System.Text.Encoding.Default.GetString(array))

                End Using
                Using fstream2 As FileStream = File.OpenRead(Environ("TEMP") & "/vidjet.workw")
                    'преобразуем строку в байты
                    Dim array As Byte() = New Byte(fstream2.Length) {}
                    'чтение данных
                    fstream2.Read(array, 0, array.Length)
                    'декодируем байты в строку
                    work_w = Integer.Parse(System.Text.Encoding.Default.GetString(array))

                End Using
            End If

        Catch ex As Exception

        End Try

        global_wpf_form.Left = work_w - 450
        global_wpf_form.Top = work_h - 524


        'конец

        'кнопка закрытия
        endbutton.Visibility = True
        'конец

        'кнопка приоритета окна
        topframebutton.Visibility = True
        'конец



        'конец

        'приветствие
        Dim bi3 As New BitmapImage
        bi3.BeginInit()
        bi3.UriSource = New Uri(Environment.CurrentDirectory + "\@tyan\tyan_tab.png", UriKind.Absolute)
        bi3.EndInit()
        tyan_tab.Source = bi3

        girl_config = IO.File.ReadAllLines(Environment.CurrentDirectory + "\@tyan\config.txt", System.Text.Encoding.Default)
        girlnamebox.Content = girl_config(0)
        girlnamebox.Foreground = New BrushConverter().ConvertFrom(girl_config(1))
        Dim subloadstring As String = "Как тебя зовут?"
        dbox.Content = subloadstring
        'конец

        'меню планшета
        namebox.Visibility = False
        namebutton.Visibility = False
        clock.Visibility = True
        date_clock.Visibility = True
        clockbutton.Visibility = True
        cpubutton.Visibility = True
        memorybutton.Visibility = True
        memory.Visibility = True
        help.Visibility = True
        setting.Visibility = True
        'конец

        'старт таймера обновления
        AddHandler updater.Tick, AddressOf updater_Tick
        updater.Interval = New TimeSpan(0, 0, 1)
        updater.Start()
        'конец

        'нагрузка цп
        usecpu.CategoryName = "Processor"
        usecpu.CounterName = "% Processor Time"
        usecpu.InstanceName = "_Total"
        'конец

        'заполненность оперативной памяти
        usememory.CategoryName = "память"
        usememory.CounterName = "% использования выделенной памяти"
        'конец 
    End Sub

    Private Sub clockbutton_Click(sender As Object, e As RoutedEventArgs) Handles clockbutton.Click
        namebox.Visibility = True
        namebutton.Visibility = True
        clock.Visibility = False
        date_clock.Visibility = False
        cpu.Visibility = True
        memory.Visibility = True
        dbox.Content = "Текущее время."
    End Sub

    Private Sub cpubutton_Click(sender As Object, e As RoutedEventArgs) Handles cpubutton.Click
        namebox.Visibility = True
        namebutton.Visibility = True
        clock.Visibility = True
        date_clock.Visibility = True
        cpu.Visibility = False
        memory.Visibility = True
        dbox.Content = "Загруженность твоего процессора."
    End Sub

    Private Sub memorybutton_Click(sender As Object, e As RoutedEventArgs) Handles memorybutton.Click
        namebox.Visibility = True
        namebutton.Visibility = True
        clock.Visibility = True
        date_clock.Visibility = True
        cpu.Visibility = True
        memory.Visibility = False
        dbox.Content = "Заполненность твоей оперативной памяти."
    End Sub

    Private Sub help_Click(sender As Object, e As RoutedEventArgs) Handles help.Click
        MsgBox(("Модификация спрайтов:") + vbCrLf + ("vk.com/toptopol - Артур 'ТопТополь' Дворецкий") + vbCrLf + ("vk.com/eugene_deli - link (Eugene Deli)") + vbCrLf + ("vk.com/rambarrett - link (Vyacheslav RamBarrett)") + vbCrLf + ("Программист:") + vbCrLf + ("vk.com/myonassalat - (den)") + vbCrLf + ("Спонсор:") + vbCrLf + ("vk.com/kunsite - (Lord Kunzite) и проект DVA") + vbCrLf + ("vk.com/dva_mods"))
    End Sub

    Private Sub setting_Click(sender As Object, e As RoutedEventArgs) Handles setting.Click


        If setting_button_press = True Then
            dbox.Content = "Привет, " & USERNAME & "."

            endbutton.Visibility = True

            topframebutton.Visibility = True

            setting_button_press = True

        End If

        If setting_button_press = False Then
            dbox.Content = "Выбери тян."

            endbutton.Visibility = False

            topframebutton.Visibility = False

            setting_button_press = False

        End If

        If setting_button_press = False Then
            setting_button_press = True
        ElseIf setting_button_press = True Then
            setting_button_press = False
        End If

    End Sub
    'события смены спрайтов


    'конец






    Private Sub endbutton_Click(sender As Object, e As RoutedEventArgs) Handles endbutton.Click
        End
    End Sub

    Private Sub topframebutton_Click(sender As Object, e As RoutedEventArgs) Handles topframebutton.Click
        If global_wpf_form.Topmost = True Then
            global_wpf_form.Topmost = False
        ElseIf global_wpf_form.Topmost = False Then
            global_wpf_form.Topmost = True
        End If

    End Sub



    'события фраз


    Private Sub MainWindow_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded

        Try

            IO.File.WriteAllText(Environ("TEMP") & "/vidjet.workh", "")
            IO.File.WriteAllText(Environ("TEMP") & "/vidjet.workw", "")
            'запись в файл
            Using fstream1 As New FileStream(Environ("TEMP") & "/vidjet.workh", FileMode.OpenOrCreate)

                'преобразуем строку в байты
                Dim array As Byte() = System.Text.Encoding.Default.GetBytes(work_h)
                'записываем массив байтов в файл
                fstream1.Write(array, 0, array.Length)
            End Using
            Using fstream2 As New FileStream(Environ("TEMP") & "/vidjet.workw", FileMode.OpenOrCreate)
                Dim array As Byte() = System.Text.Encoding.Default.GetBytes(work_w)
                'записываем массив байтов в файл
                fstream2.Write(array, 0, array.Length)
            End Using

        Catch ex As Exception

        End Try
    End Sub



    Private Sub dialog_img_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles dialog_img.MouseLeftButtonDown
        Me.DragMove()
    End Sub

    Private Sub girlnamebox_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles girlnamebox.MouseLeftButtonDown
        Me.DragMove()
    End Sub

    Private Sub dbox_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles dbox.MouseLeftButtonDown
        Me.DragMove()
    End Sub

    Private Sub tyan_tab_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles tyan_tab.MouseLeftButtonDown
        dbox.Content = Replace(Replace(girl_config(phrase), "USERNAME", USERNAME), "&", vbCrLf)

        phrase = phrase + 1
                           If phrase = girl_config.Count Then
                               phrase = 2
                           End If
                       End Sub
End Class










