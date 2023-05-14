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

        public async Task<DocumentDTO> CreateDocument(int userId, NewDocumentDto newDocumentDto)
        {
            var documentEntity = _mapper.Map<DAL.Entities.Document>(newDocumentDto);
            documentEntity.UserId = userId;
            _context.Documents.Add(documentEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<DocumentDTO>(documentEntity);
        }

        public async Task<DocumentDTO> CreateWordFile(int documentId)
        {
            var documentEntity = await _context.Documents.FirstOrDefaultAsync(doc => doc.Id == documentId);
            if (documentEntity is null)
            {
                return null;
            }
            else
            {
                try
                {
                    object filename = documentEntity.Name + ".docx";
                    Document document = new Document();
                    Section section = document.AddSection();

                    ParagraphStyle headerStyle = new ParagraphStyle(document);
                    headerStyle.Name = "Header style";
                    headerStyle.CharacterFormat.FontSize = 14;
                    document.Styles.Add(headerStyle);

                    ParagraphStyle textStyle = new ParagraphStyle(document);
                    textStyle.Name = "Text style";
                    textStyle.CharacterFormat.FontSize = 11;
                    document.Styles.Add(textStyle);


                    Paragraph projectAimHeader = section.AddParagraph();
                    projectAimHeader.Format.HorizontalAlignment = HorizontalAlignment.Center;
                    projectAimHeader.AppendText(BuiltinStyle.Heading2.ToString());
                    string projectAimHeaderText = "Aim of the project";
                    projectAimHeader.Text = projectAimHeaderText;
                    projectAimHeader.ApplyStyle(headerStyle.Name);

                    Paragraph projectAimParagraph = section.AddParagraph();
                    projectAimParagraph.Format.HorizontalAlignment = HorizontalAlignment.Left;
                    if (documentEntity.ProjectAim != null)
                    {
                        string projectAimText = documentEntity.ProjectAim;
                        projectAimHeader.Text = projectAimHeaderText;
                    }
                    projectAimParagraph.ApplyStyle(textStyle.Name);

                    Paragraph glossaryHeader = section.AddParagraph();
                    string glossaryHeaderText = "Glossary";
                    glossaryHeader.Text = glossaryHeaderText;
                    glossaryHeader.Format.HorizontalAlignment = HorizontalAlignment.Center;
                    glossaryHeader.ApplyStyle(headerStyle.Name);

                    Paragraph glossaryParagraph = section.AddParagraph();
                    var documentGlossary = await _context.Glossaries
                        .Where(g => g.DocumentId == documentEntity.Id)
                        .ToListAsync();
                    if (documentGlossary != null)
                    {
                        List<string> glossaryTableHeaders = new List<string>();
                        glossaryTableHeaders.Add("Term (abbreviation)");
                        glossaryTableHeaders.Add("Definition");
                        List<List<string>> glossaryTableData = new List<List<string>>();
                        foreach (var glossary in documentGlossary)
                        {
                            List<string> glossaryRow = new List<string>();
                            glossaryRow.Add(glossary.Term);
                            glossaryRow.Add(glossary.Definition);
                            glossaryTableData.Add(glossaryRow);
                        }

                        Table glossaryTable = section.AddTable(true);
                        glossaryTable.ResetCells(glossaryTableData.Count + 1, glossaryTableHeaders.Count);
                        TableRow FRow = glossaryTable.Rows[0];
                        FRow.IsHeader = true;
                        for (int i = 0; i < glossaryTableHeaders.Count; i++)
                        {
                            TextRange TR = glossaryParagraph.AppendText(glossaryTableHeaders[i]);
                            TR.CharacterFormat.FontSize = 11;
                            TR.CharacterFormat.Bold = true;
                        }

                        for (int r = 0; r < glossaryTableData.Count; r++)
                        {
                            TableRow DataRow = glossaryTable.Rows[r + 1];
                            for (int c = 0; c < glossaryTableData[r].Count; c++)
                            {
                                Paragraph glossaryPar = DataRow.Cells[c].AddParagraph();
                                TextRange TR2 = glossaryPar.AppendText(glossaryTableData[r][c]);
                                TR2.CharacterFormat.FontSize = 11;
                            }
                        }
                    }

                    Paragraph funcRequirementsHeader = section.AddParagraph();
                    string funcRequirementsHeaderText = "Functional requirements";
                    funcRequirementsHeader.Text = funcRequirementsHeaderText;
                    funcRequirementsHeader.Format.HorizontalAlignment = HorizontalAlignment.Center;
                    funcRequirementsHeader.ApplyStyle(headerStyle.Name);

                    //Microsoft.Office.Interop.Word.Paragraph funcRequirementsParagraph = document.Content.Paragraphs.Add();
                    //funcRequirementsParagraph.Range.Font.Size = 11;
                    //if (documentEntity.UserStories != null)
                    //{
                    //    var userStories = documentEntity.UserStories.ToList();
                    //    List<string> userStoryText = new List<string>();
                    //    string para6Text = "";
                    //    int userStoriesCount = 1;
                    //    foreach (var userStory in userStories)
                    //    {
                    //        string usName = "User story " + userStoriesCount.ToString() + ". " + userStory.Name + "\n";
                    //        string usText = "";
                    //        foreach (var item in userStory.Formulas)
                    //        {
                    //            usText += item;
                    //            usText += "\n";
                    //        }
                    //        string usCriteria = "Acceptance criteria:\n";
                    //        int count = 1;
                    //        foreach (var item in userStory.AcceptanceCriterias)
                    //        {
                    //            string criteria = count.ToString() + ". " + item + "\n";
                    //            usCriteria += criteria;
                    //            count++;
                    //        }
                    //        string usResult = usName + usText + usCriteria;
                    //        para6Text += usResult;
                    //    }
                    //    funcRequirementsParagraph.Range.Text = para6Text;
                    //}
                    //funcRequirementsParagraph.Range.InsertParagraphAfter();

                    document.SaveToFile("LocalFiles/" + filename, FileFormat.Docx);
                    return _mapper.Map<DocumentDTO>(documentEntity);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

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
                //for (int i = 0; i < raciMatrixTableHeaders.Count; i++)
                //{
                //    TextRange TR = raciParagraph.AppendText(raciMatrixTableHeaders[i]);
                //    TR.CharacterFormat.FontSize = 11;
                //    TR.CharacterFormat.Bold = true;
                //}

                //for (int r = 0; r < raciMatrix.RACI.Count; r++)
                //{
                //    TableRow DataRow = raciTable.Rows[r + 1];
                //    for (int c = 0; c < raciMatrix.RACI[r].Count; c++)
                //    {
                //        Paragraph glossaryPar = DataRow.Cells[c].AddParagraph();
                //        TextRange TR3 = glossaryPar.AppendText(raciMatrix.Tasks[i]);
                //        TextRange TR2 = glossaryPar.AppendText(raciMatrix.RACI[r][c].ToString());
                //        TR2.CharacterFormat.FontSize = 11;
                //    }
                //}

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
    }
}
