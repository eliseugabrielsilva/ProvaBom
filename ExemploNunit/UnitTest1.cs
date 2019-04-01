using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Drawing;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System.Threading;
using System.Diagnostics;

//Notas: Alterar patch do Chomedriver linha 25


namespace Bom
{


    public class CadastroLogin
    {
        [Test]
        public void ValidaCadLog()
        {
            //Configuração do Ambiente CHROME e chamada da URL
            //IWebDriver driver = new ChromeDriver();
            IWebDriver driver = new ChromeDriver(@"C:\Users\eliseu.silva\.nuget\packages\selenium.chrome.webdriver\2.45.0\driver");
            //Dev
            driver.Navigate().GoToUrl("https://demo.bompracredito.com.br/emprestimo-pessoal/");
            
            driver.Manage().Window.Maximize();
            
      
            Thread.Sleep(2000);
            //
            //Gerar de CPF
            int soma = 0, resto = 0;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            Random rnd = new Random();
            string semente = rnd.Next(100000000, 999999999).ToString();
            for (int i = 0; i < 9; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            semente = semente + resto;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            semente = semente + resto;
            //return semente;
            Console.WriteLine(semente);
            //Seleciona Valor e opção de parcelamento
            driver.FindElement(By.Id("btnTwoThousand")).Click();
            driver.FindElement(By.Id("btnNineTerm")).Click();
            //Inserção e Validação de valor inválido e evidencia
            driver.FindElement(By.Id("name")).SendKeys("teste");
            var valnomein = driver.FindElement(By.ClassName("error-message"));
            valnomein.Text.Equals("Por favor, informe seu nome completo");
           
            driver.FindElement(By.Id("name")).SendKeys("teste teste ok");
            driver.FindElement(By.Id("email")).SendKeys("teste teste ok");
            valnomein.Text.Equals("E - mail inválido");
            
            Thread.Sleep(3000);
            driver.FindElement(By.Id("email")).Clear();
            var email = driver.FindElement(By.Id("email"));
            driver.FindElement(By.Id("email")).SendKeys(semente + "testebana@gmail.com");
            email.SendKeys(Keys.Tab);
            Thread.Sleep(2000);
            driver.FindElement(By.Id("btnContinue")).Click();
            Thread.Sleep(9000);
            // Cadastro invalido CPF
            var cpf = driver.FindElement(By.Id("borrower.cpf"));
            cpf.SendKeys("111111");
            cpf.Text.Equals("CPF inválido");
            // Cadastro invalido Data de Nascimento
            var datan = driver.FindElement(By.Id("borrower.dateOfBirth"));
            datan.SendKeys("11/11/1111");
            datan.Text.Equals("Data inválida.");
            // Cadastro invalido De Valor
            var valor = driver.FindElement(By.Id("borrower.monthlyGrossIncome"));
            valor.SendKeys("00000");
            valor.Text.Equals("Informe sua renda corretamente.");
            Screenshot dadosinv = ((ITakesScreenshot)driver).GetScreenshot();
            dadosinv.SaveAsFile((@"\Users\eliseu.silva\evidencia") + ("DadosInvalidosOK.jpg"));
            
            Thread.Sleep(2000);

            //Cadastro válido
            cpf.Clear();
            datan.Clear();
            valor.Clear();
            cpf.SendKeys(semente);
            Thread.Sleep(2000);
            datan.SendKeys("21/09/1988");
            Thread.Sleep(2000);
            valor.SendKeys("15000");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("borrower.gender")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("borrower.maritalStatus2")).Click();
            Thread.Sleep(2000);
            //Variaveis Profissão e Ocupação
            var ocupa = driver.FindElement(By.Id("borrower.jobType"));
            var assal = driver.FindElement(By.Id("borrower.profession"));
            var ocp1 = "Assalariado";
            var ocp2 = "Empresário";
            var ocp3 = "Estudante";
            var pro1 = "Desenhista";
            var pro2 = "Piscicultor";

            ocupa.SendKeys(ocp1);
            Thread.Sleep(2000);
            assal.SendKeys(pro1);
            Thread.Sleep(2000);
            ocupa.SendKeys(ocp2);
            Thread.Sleep(2000);
            assal.SendKeys(pro2);
            Thread.Sleep(2000);
            ocupa.SendKeys(ocp3);
            Thread.Sleep(2000);
            //assal.SendKeys(pro3);
            //Thread.Sleep(2000);
            //var pro3 = "Estudante";
            driver.FindElement(By.Id("borrower.educationLevel")).SendKeys("Analfabeto");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("borrower.bankingData.bankNumber")).SendKeys("Outros");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("borrower.bankingData.hasCheckbook")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("hasProperty")).Click();
            driver.FindElement(By.Id("borrower.hasNegativeCreditRecord2")).Click();
            driver.FindElement(By.Id("hasVehicle2")).Click();

            Thread.Sleep(2000);
            driver.FindElement(By.ClassName("caption-left")).Click();
            Thread.Sleep(9000);
            driver.FindElement(By.Id("homeAddress.cep")).SendKeys("05782440");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("homeAddress.number")).SendKeys("2");
            driver.FindElement(By.Id("mobilePhone")).Clear();
            Thread.Sleep(2000);
            driver.FindElement(By.Id("mobilePhone")).SendKeys("11986360527");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            try

            {
                driver.FindElement(By.Id("loanObjective")).SendKeys("Viajar");

            }

            catch (Exception)

            {

                Console.WriteLine("Elemento não localizado");

            }

            
            
            //driver.FindElement(By.Id("loanObjective")).SendKeys("Viajar");
            //emp.Equals("loanObjective");


            //OpenQA.Selenium.Support.UI.IWait<IWebDriver> wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //OpenQA.Selenium.IWebElement element = wait.Until<TimeSpan>((By.Id("loanObjective")).SendKeys("Viajar"));
            //IWait<IWebDriver> wait driver.FindElement, (By.Id("loanObjective")).SendKeys("Viajar"));







            driver.FindElement(By.Id("button-borrower-info")).Submit();



            //Valida redirec correto se esta pagina esperada
            Thread.Sleep(20000);
            var valired = driver.Url.Equals("https://demo.bompracredito.com.br/v2/cadastrocompletoX2222XXX");
            //Retorna inicio
            driver.Navigate().GoToUrl("https://demo.bompracredito.com.br/emprestimo-pessoal/");
            //driver.Url.Contains(https://demo.bompracredito.com.br/v2/cadastrocompleto);

            driver.Quit();
        }

        
            }

        }

