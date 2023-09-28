using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Vestibular
{
    class Program
    {
        public struct Dados
        {
            public string nome;
            public int inscricao;
            public string curso;
            public double nota;

            public Dados(string n, int m, string c, double no)
            {
                nome = n;
                inscricao = m;
                curso = c;
                nota = no;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Análise de dados de um Vestibular");
            Console.Write("\nInforme a quantidade de alunos: ");
            int alunos = int.Parse(Console.ReadLine());
            Console.Write("\nInforme a quantidade de cursos: ");
            int cursos = int.Parse(Console.ReadLine());
            while (cursos < 2)
            {
                Console.Write("\nInforme a quantidade de cursos: ");
                cursos = int.Parse(Console.ReadLine());
            }
            string[] curso = new string[cursos];
            for (int i = 0; i < cursos; i++)
            {
                Console.Write("\nInforme o {0}º curso: ", i + 1);
                curso[i] = Console.ReadLine().ToUpper();
                for (int k = 0; k < i; k++)
                {
                    while (curso[i] == curso[k])
                    {
                        Console.WriteLine("Curso já informado!");
                        Console.Write("\nInforme o {0}º curso: ", i + 1);
                        curso[i] = Console.ReadLine().ToUpper();
                    }
                }
            }
            int[] vagas = new int[cursos];
            for (int i = 0; i < cursos; i++)
            {
                Console.Write("\nInforme a quantidade de vagas do curso {0}: ", curso[i]);
                vagas[i] = int.Parse(Console.ReadLine());
            }
            Dados[] dados = new Dados[alunos];
            for (int i = 0; i < alunos; i++)
            {
                Console.Write("\nInforme o nome do {0}º candidato: ", i + 1);
                dados[i].nome = Console.ReadLine().ToUpper();
                Console.Write("Informe o número de inscrição do(a) {0}: ", dados[i].nome);
                dados[i].inscricao = int.Parse(Console.ReadLine());
                for (int k = 0; k < i; k++)
                {
                    while (dados[i].inscricao == dados[k].inscricao)
                    {
                        Console.WriteLine("\nNúmero de Inscrição já informado!\n");
                        Console.Write("Informe o número de inscrição do(a) {0}: ", dados[i].nome);
                        dados[i].inscricao = int.Parse(Console.ReadLine());
                    }
                }
                Console.Write("Informe a opção de curso do(a) {0}: ", dados[i].nome);
                dados[i].curso = Console.ReadLine().ToUpper();
                while (!curso.Contains(dados[i].curso))
                {
                    Console.WriteLine("\nCurso inexistente!\n");
                    Console.Write("Informe a opção de curso do(a) {0}: ", dados[i].nome);
                    dados[i].curso = Console.ReadLine().ToUpper();
                }
                Console.Write("Informe a nota do(a) {0}: ", dados[i].nome);
                dados[i].nota = double.Parse(Console.ReadLine());
                while (dados[i].nota < 0 || dados[i].nota > 100)
                {
                    Console.WriteLine("\nA nota deve estar entre 0 e 100!\n");
                    Console.Write("Informe a nota do(a) {0}: ", dados[i].nome);
                    dados[i].nota = double.Parse(Console.ReadLine());
                }
            }
            string path = "vestibular.txt";
            StreamWriter wr = new StreamWriter(path);
            for (int i = 0; i < alunos; i++)
            {
                wr.WriteLine(dados[i].nome + ";" + dados[i].inscricao + ";" + dados[i].curso + ";" + dados[i].nota);
            }
            Console.WriteLine("\n\nArquivo gravado com sucesso.");
            wr.Close();

            //-- LER DO ARQUIVO --
            if (!File.Exists(path))
            {
                Console.WriteLine("\n\nERRO. Arquivo inexistente\n\n");
            }
            else
            {
                StreamReader rd = new StreamReader(path);
                Dados[] vindo_do_arquivo = new Dados[File.ReadAllLines("vestibular.txt").Length]; // quantidade de linhas do arquivo
                int count = 0;
                while (!rd.EndOfStream)
                {
                    //lê uma linha do arquivo
                    string linha = rd.ReadLine();

                    //lê o conteúdo da linha, separando os dados a partir do ;
                    string[] z = linha.Split(';');
                    int inscricao = Convert.ToInt32(z[1]);
                    double nota = Convert.ToDouble(z[3]);
                    vindo_do_arquivo[count].nome = z[0];
                    vindo_do_arquivo[count].inscricao = inscricao;
                    vindo_do_arquivo[count].curso = z[2];
                    vindo_do_arquivo[count].nota = nota;

                    count++;
                }
                //fecha o arquivo
                rd.Close();
                Dados aux;
                StreamWriter jorge;

                for (int i = 0; i < curso.Length; i++)
                {
                    string arquivo = string.Format("aprovados_{0}.txt", curso[i]);
                    jorge = new StreamWriter(arquivo);
                    jorge.WriteLine("hello?");
                    for (int j = 0; j < vindo_do_arquivo.Length; j++)
                    {
                        string nome = vindo_do_arquivo[i].nome;
                        int inscricao = vindo_do_arquivo[i].inscricao;
                        string opcao = vindo_do_arquivo[i].curso;
                        double nota = vindo_do_arquivo[i].nota;
                        aux = new Dados(nome, inscricao, opcao, nota);

                        if (vindo_do_arquivo[j].curso == curso[i])
                        {
                            if (vindo_do_arquivo[i].inscricao != aux.inscricao)
                            {
                                if (aux.nota < vindo_do_arquivo[i].nota)
                                {
                                    aux = vindo_do_arquivo[i];
                                }
                                else if (aux.nota == vindo_do_arquivo[i].nota)
                                {
                                    if (aux.nome.CompareTo(vindo_do_arquivo[i].nome) == 1)
                                    {
                                        aux = vindo_do_arquivo[i];
                                    }
                                    else if (aux.nome.CompareTo(vindo_do_arquivo[i].nome) == 0)
                                    {
                                        if (aux.inscricao > vindo_do_arquivo[i].inscricao)
                                        {
                                            aux = vindo_do_arquivo[i];
                                        }
                                    }
                                }
                            }
                        }
                        jorge.Write("{0}º: {1}", j + 1, aux.nome);
                    }

                }
            }
            Console.WriteLine("\n\nArquivo de aprovados gravado!\n\n");
            Console.ReadKey();
        }
    }
}

