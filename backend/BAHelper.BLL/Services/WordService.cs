using AutoMapper;
using BAHelper.BLL.Services.Abstract;
using BAHelper.Common;
using BAHelper.Common.DTOs.Document;
using BAHelper.Common.Services;
using BAHelper.DAL.Context;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Npgsql.Internal.TypeHandlers.LTreeHandlers;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System.Data;
using System.Net;
using System.Web;

namespace BAHelper.BLL.Services
{
    public class WordService : BaseService
    {
        public WordService(BAHelperDbContext context, IMapper mapper)
            :base (context, mapper) { }

        public async Task<DocumentDTO> CreateWordFile(int documentId)
        {
            var documentEntity = await _context
                .Documents
                .Include(doc => doc.Glossary)
                .Include(doc => doc.UserStories)
                .FirstOrDefaultAsync(doc => doc.Id == documentId);
            if (documentEntity is null)
            {
                return null;
            }
            try
            {
                object filename = documentEntity.Name + ".docx";
                Document document = new Document();
                Section section1 = document.AddSection();

                Paragraph documentHeader = section1.AddParagraph();
                documentHeader.Format.AfterAutoSpacing = false;
                documentHeader.Format.AfterSpacing = 15;
                string documentHeaderText = documentEntity.Name;
                TextRange documentHeaderTR = documentHeader.AppendText(documentHeaderText);
                documentHeader.Format.HorizontalAlignment = HorizontalAlignment.Center;
                documentHeaderTR.CharacterFormat.FontSize = 18;
                documentHeaderTR.CharacterFormat.Bold = true;

                Paragraph contentHeader = section1.AddParagraph();
                contentHeader.Format.HorizontalAlignment = HorizontalAlignment.Left;
                contentHeader.Format.AfterAutoSpacing = false;
                contentHeader.Format.AfterSpacing = 25;
                string contentHeaderText = "Table of content";
                TextRange contentHeaderTR = contentHeader.AppendText(contentHeaderText);
                contentHeaderTR.CharacterFormat.FontSize = 14;
                contentHeaderTR.CharacterFormat.Bold = true;

                Paragraph contentParagraph = section1.AddParagraph();
                contentParagraph.Format.HorizontalAlignment = HorizontalAlignment.Left;
                string contentParagraphText = "1. Aim of the project\n" +
                                              "2. Glossary\n" +
                                              "3. User stories (Use cases)";
                TextRange contentParagraphTR = contentParagraph.AppendText(contentParagraphText);
                contentParagraphTR.CharacterFormat.FontSize = 14;

                Section section2 = document.AddSection();
                Paragraph projectAimHeader = section2.AddParagraph();
                projectAimHeader.Format.HorizontalAlignment = HorizontalAlignment.Center;
                projectAimHeader.Format.AfterAutoSpacing = false;
                projectAimHeader.Format.AfterSpacing = 10;
                string projectAimHeaderText = "Project aim";
                TextRange projectAimHeaderTR = projectAimHeader.AppendText(projectAimHeaderText);
                projectAimHeaderTR.CharacterFormat.FontSize = 14;
                projectAimHeaderTR.CharacterFormat.Bold = true;

                Paragraph projectAimParagraph = section2.AddParagraph();
                projectAimParagraph.Format.HorizontalAlignment = HorizontalAlignment.Left;
                projectAimParagraph.Format.AfterAutoSpacing = false;
                projectAimHeader.Format.AfterSpacing = 20;
                string projectAimText = documentEntity.ProjectAim;
                TextRange projectAimTR = projectAimParagraph.AppendText(projectAimText);
                projectAimTR.CharacterFormat.FontSize = 14;

                Paragraph glossaryHeader = section2.AddParagraph();
                string glossaryHeaderText = "Glossary";
                glossaryHeader.Format.HorizontalAlignment = HorizontalAlignment.Center;
                glossaryHeader.Format.AfterAutoSpacing = false;
                glossaryHeader.Format.AfterSpacing = 10;
                TextRange glossaryHeaderTR = glossaryHeader.AppendText(glossaryHeaderText);
                glossaryHeaderTR.CharacterFormat.FontSize = 14;
                glossaryHeaderTR.CharacterFormat.Bold = true;

                if (documentEntity.Glossary != null)
                {
                    Paragraph glossaryParagraph = section2.AddParagraph();
                    List<string> glossaryTableHeaders = new List<string> { "Term", "Definition" };
                    Table glossaryTable = section2.AddTable(true);
                    glossaryTable.ResetCells(documentEntity.Glossary.Count + 1, 2);
                    TableRow FRow = glossaryTable.Rows[0];
                    FRow.IsHeader = true;
                    for (int i = 0; i < glossaryTableHeaders.Count; i++)
                    {
                        Paragraph p = FRow.Cells[i].AddParagraph();
                        FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        p.Format.HorizontalAlignment = HorizontalAlignment.Center;
                        TextRange TR = p.AppendText(glossaryTableHeaders[i]);
                        TR.CharacterFormat.FontSize = 12;
                        TR.CharacterFormat.Bold = true;
                    }
                    for (int r = 0; r < documentEntity.Glossary.Count; r++)
                    {
                        TableRow DataRow = glossaryTable.Rows[r + 1];
                        DataRow.Height = 20;
                        for (int c = 0; c < 2; c++)
                        {
                            if (c == 0)
                            {
                                DataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                Paragraph p2 = DataRow.Cells[c].AddParagraph();
                                TextRange TR2 = p2.AppendText(documentEntity.Glossary[r].Term);
                                p2.Format.HorizontalAlignment = HorizontalAlignment.Center;
                                TR2.CharacterFormat.FontSize = 11;
                            }
                            else
                            {
                                DataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                                Paragraph p2 = DataRow.Cells[c].AddParagraph();
                                TextRange TR2 = p2.AppendText(documentEntity.Glossary[r].Definition);
                                p2.Format.HorizontalAlignment = HorizontalAlignment.Center;
                                TR2.CharacterFormat.FontSize = 11;
                            }
                        }
                    }
                }
                Section section3 = document.AddSection();
                Paragraph funcRequirementsHeader = section3.AddParagraph();
                string funcRequirementsHeaderText = "User stories";
                funcRequirementsHeader.Format.HorizontalAlignment = HorizontalAlignment.Center;
                funcRequirementsHeader.Format.AfterAutoSpacing = false;
                funcRequirementsHeader.Format.AfterSpacing = 10;
                TextRange funcHeaderTR = funcRequirementsHeader.AppendText(funcRequirementsHeaderText);
                funcHeaderTR.CharacterFormat.FontSize = 14;
                funcHeaderTR.CharacterFormat.Bold = true;

                if (documentEntity.UserStories != null)
                {
                    int userStoryIndex = 1;
                    foreach (var us in documentEntity.UserStories)
                    {
                        Paragraph userStoryHeader = section3.AddParagraph();
                        string userStoryHeaderText = $"User story {userStoryIndex}. {us.Name}";
                        userStoryHeader.Format.HorizontalAlignment = HorizontalAlignment.Left;
                        userStoryHeader.Format.AfterAutoSpacing = false;
                        userStoryHeader.Format.AfterSpacing = 10;
                        TextRange userStoryHeaderTR = userStoryHeader.AppendText(userStoryHeaderText);
                        userStoryHeaderTR.CharacterFormat.FontSize = 14;
                        userStoryHeaderTR.CharacterFormat.Bold = true;
                        userStoryHeaderTR.CharacterFormat.Italic = true;

                        foreach (var formula in us.Formulas)
                        {
                            Paragraph usFormula = section3.AddParagraph();
                            string usFormulaText = formula;
                            usFormula.Format.HorizontalAlignment = HorizontalAlignment.Left;
                            usFormula.Format.AfterAutoSpacing = false;
                            usFormula.Format.AfterSpacing = 10;
                            TextRange usFormulaTR = usFormula.AppendText(usFormulaText);
                            usFormulaTR.CharacterFormat.FontSize = 14;
                        }

                        Paragraph acceptanceCriteriaHeader = section3.AddParagraph();
                        string acceptanceCriteriaHeaderText = "Acceptance criteria";
                        acceptanceCriteriaHeader.Format.HorizontalAlignment = HorizontalAlignment.Left;
                        acceptanceCriteriaHeader.Format.AfterAutoSpacing = false;
                        acceptanceCriteriaHeader.Format.AfterSpacing = 10;
                        TextRange acceptanceCriteriaHeaderTR = acceptanceCriteriaHeader.AppendText(acceptanceCriteriaHeaderText);
                        acceptanceCriteriaHeaderTR.CharacterFormat.FontSize = 14;
                        acceptanceCriteriaHeaderTR.CharacterFormat.Bold = true;
                        acceptanceCriteriaHeaderTR.CharacterFormat.Italic = true;

                        int criteriaIndex = 1;
                        foreach (var criteria in us.AcceptanceCriterias)
                        {
                            Paragraph usCriteria = section3.AddParagraph();
                            string usCriteriaText = $"\t{criteriaIndex}. {criteria}";
                            usCriteria.Format.HorizontalAlignment = HorizontalAlignment.Left;
                            usCriteria.Format.AfterAutoSpacing = false;
                            usCriteria.Format.AfterSpacing = 10;
                            TextRange usCriteriaTR = usCriteria.AppendText(usCriteriaText);
                            usCriteriaTR.CharacterFormat.FontSize = 14;
                            criteriaIndex++;
                        }
                        userStoryIndex++;
                    }
                }

                document.SaveToFile("LocalFiles/" + filename, FileFormat.Docx);
                return _mapper.Map<DocumentDTO>(documentEntity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CreateRaciMatrixFile(RaciMatrix raciMatrix)
        {
            try
            {
                object filename = "RaciMatrix.docx";
                Document document = new Document();
                Section section = document.AddSection();

                ParagraphStyle headerStyle = new ParagraphStyle(document);
                headerStyle.Name = "Header style";
                headerStyle.CharacterFormat.FontSize = 14;
                document.Styles.Add(headerStyle);

                Paragraph raciMatrixHeader = section.AddParagraph();
                raciMatrixHeader.Format.HorizontalAlignment = HorizontalAlignment.Center;
                raciMatrixHeader.AppendText(BuiltinStyle.Heading2.ToString());
                string raciMatrixText = "RACI Matrix";
                raciMatrixHeader.Text = raciMatrixText;
                raciMatrixHeader.ApplyStyle(headerStyle.Name);

                Paragraph raciParagraph = section.AddParagraph();
                List<string> raciMatrixTableHeaders = new List<string>();
                raciMatrixTableHeaders.Add("");
                foreach(var executor in raciMatrix.Executors)
                {
                    raciMatrixTableHeaders.Add(executor);
                }

                Table raciTable = section.AddTable(true);
                raciTable.ResetCells(raciMatrix.RACI.Count + 1, raciMatrixTableHeaders.Count);
                TableRow FRow = raciTable.Rows[0];
                FRow.IsHeader = true;

                for (int i = 0; i < raciMatrixTableHeaders.Count; i++)
                {
                    Paragraph p = FRow.Cells[i].AddParagraph();
                    FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    p.Format.HorizontalAlignment = HorizontalAlignment.Center;
                    TextRange TR = p.AppendText(raciMatrixTableHeaders[i]);
                    TR.CharacterFormat.FontSize = 12;
                    TR.CharacterFormat.Bold = true;
                }

                for (int r = 0; r < raciMatrix.RACI.Count; r++)
                {
                    TableRow DataRow = raciTable.Rows[r+1];
                    DataRow.Height = 20;
                    for (int c = 0; c < raciMatrix.RACI[r].Count+1; c++)
                    {
                        if (c == 0)
                        {
                            DataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            Paragraph p2 = DataRow.Cells[c].AddParagraph();
                            TextRange TR3 = p2.AppendText(raciMatrix.Tasks[r]);
                            p2.Format.HorizontalAlignment = HorizontalAlignment.Center;
                            TR3.CharacterFormat.FontSize = 11;
                        }
                        else
                        {
                            DataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                            Paragraph p2 = DataRow.Cells[c].AddParagraph();
                            TextRange TR2 = p2.AppendText(raciMatrix.RACI[r][c-1].ToString());
                            p2.Format.HorizontalAlignment = HorizontalAlignment.Center;
                            TR2.CharacterFormat.FontSize = 11;
                        }
                    }
                }
                document.SaveToFile("LocalFiles/" + filename, FileFormat.Docx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return;
        }

        public async Task CreateCommunicationPlan(List<CommunicationPlan> plan)
        {
            try
            {
                object filename = "CommunicationPlan.docx";
                Document document = new Document();
                Section section = document.AddSection();

                ParagraphStyle headerStyle = new ParagraphStyle(document);
                headerStyle.Name = "Header style";
                headerStyle.CharacterFormat.FontSize = 14;
                document.Styles.Add(headerStyle);

                Paragraph communicationPlanHeader = section.AddParagraph();
                communicationPlanHeader.Format.HorizontalAlignment = HorizontalAlignment.Center;
                communicationPlanHeader.AppendText(BuiltinStyle.Heading2.ToString());
                string communicationPlanText = "Communication plan";
                communicationPlanHeader.Text = communicationPlanText;
                communicationPlanHeader.ApplyStyle(headerStyle.Name);

                Paragraph comParagraph = section.AddParagraph();
                List<string> comTableHeaders = new List<string>
                {
                    "Опис",
                    "Частота",
                    "Канал",
                    "Аудиторія",
                    "Організатор"
                };

                Table comTable = section.AddTable(true);
                comTable.ResetCells(plan.Count + 1, comTableHeaders.Count);
                TableRow FRow = comTable.Rows[0];
                FRow.IsHeader = true;

                for (int i = 0; i < comTableHeaders.Count; i++)
                {
                    Paragraph p = FRow.Cells[i].AddParagraph();
                    FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    p.Format.HorizontalAlignment = HorizontalAlignment.Center;
                    TextRange TR = p.AppendText(comTableHeaders[i]);
                    TR.CharacterFormat.FontSize = 12;
                    TR.CharacterFormat.Bold = true;
                }

                for (int r = 0; r < plan.Count; r++)
                {
                    TableRow DataRow = comTable.Rows[r + 1];
                    DataRow.Height = 20;
                    for (int c = 0; c < 5; c++)
                    {
                        DataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                        Paragraph p2 = DataRow.Cells[c].AddParagraph();
                        switch (c)
                        {
                            case 0:
                                TextRange TR1 = p2.AppendText(plan[r].Description);
                                TR1.CharacterFormat.FontSize = 11;
                                break;
                            case 1:
                                TextRange TR2 = p2.AppendText(plan[r].Frequency);
                                TR2.CharacterFormat.FontSize = 11;
                                break;
                            case 2:
                                TextRange TR3 = p2.AppendText(plan[r].Channel);
                                TR3.CharacterFormat.FontSize = 11;
                                break;
                            case 3:
                                TextRange TR4 = p2.AppendText(plan[r].Audience);
                                TR4.CharacterFormat.FontSize = 11;
                                break;
                            case 4:
                                TextRange TR5 = p2.AppendText(plan[r].Organizer);
                                TR5.CharacterFormat.FontSize = 11;
                                break;
                        }
                        p2.Format.HorizontalAlignment = HorizontalAlignment.Center;
                    }
                }
                document.SaveToFile("LocalFiles/" + filename, FileFormat.Docx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
