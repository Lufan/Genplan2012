/*
 * Created by Луферов Александр Николаевич
 * 
 * 
 * 
 * Лицензия GNU Lesser General Public License : http://www.gnu.org/copyleft/lesser.html.
 */
using System;
using System.Collections.Generic;

using System.Text;

namespace CreateXmlConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            List<LufsGenplan.RazmType> listOfTypes = new List<LufsGenplan.RazmType>();
            //RazmType 1.1
            List<LufsGenplan.DashSpace> dsl_1_1 = new List<LufsGenplan.DashSpace>
            {
                new LufsGenplan.DashSpace(1.0d, 0.0d)
            };
            List<LufsGenplan.CategoryWidth> cd_1_1 = new List<LufsGenplan.CategoryWidth>
            {
                new LufsGenplan.CategoryWidth("I-а", 0.15),
                new LufsGenplan.CategoryWidth("I-б", 0.10),
                new LufsGenplan.CategoryWidth("II, III", 0.10),
                new LufsGenplan.CategoryWidth("IV", 0.08),
                new LufsGenplan.CategoryWidth("М6, М8", 0.10),
                new LufsGenplan.CategoryWidth("А4, А6, А8", 0.10),
                new LufsGenplan.CategoryWidth("Б4, В4, Г4, Е4, Ж4", 0.10),
                new LufsGenplan.CategoryWidth("Б2, В2, Г2", 0.10),
                new LufsGenplan.CategoryWidth("Е2, Ж2", 0.08)
            };
            List<LufsGenplan.PaintData> paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_1 = new LufsGenplan.RazmType("Разметка_1_1", "Устройство разметки 1.1", "1.1", LufsGenplan.RazmType.typeOfEntity.Line, 
                                                                     paints, cd_1_1, dsl_1_1);
            listOfTypes.Add(type_1_1);

            //RazmType 1.1 onto parkings
            List<LufsGenplan.DashSpace> dsl_1_1_st = new List<LufsGenplan.DashSpace>
            {
                new LufsGenplan.DashSpace(1.0d, 0.0d)
            };
            List<LufsGenplan.CategoryWidth> cd_1_1_st = new List<LufsGenplan.CategoryWidth>
            {
                new LufsGenplan.CategoryWidth("I-а", 0.10),
                new LufsGenplan.CategoryWidth("I-б", 0.10),
                new LufsGenplan.CategoryWidth("II, III", 0.10),
                new LufsGenplan.CategoryWidth("IV", 0.08),
                new LufsGenplan.CategoryWidth("М6, М8", 0.10),
                new LufsGenplan.CategoryWidth("А4, А6, А8", 0.10),
                new LufsGenplan.CategoryWidth("Б4, В4, Г4, Е4, Ж4", 0.10),
                new LufsGenplan.CategoryWidth("Б2, В2, Г2", 0.10),
                new LufsGenplan.CategoryWidth("Е2, Ж2", 0.08)
            };
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_1_st = new LufsGenplan.RazmType("Разметка_1_1_ст", "Устройство разметки 1.1 на стоянках", "1.1 стоянки", LufsGenplan.RazmType.typeOfEntity.Line, 
                                                                        paints, cd_1_1_st, dsl_1_1_st);
            listOfTypes.Add(type_1_1_st);

            //RazmType 1.2
            List<LufsGenplan.DashSpace> dsl_1_2 = new List<LufsGenplan.DashSpace>
            {
                new LufsGenplan.DashSpace(1.0d, 0.0d)
            };
            List<LufsGenplan.CategoryWidth> cd_1_2 = new List<LufsGenplan.CategoryWidth>
            {
                new LufsGenplan.CategoryWidth("I-а", 0.20),
                new LufsGenplan.CategoryWidth("I-б", 0.15),
                new LufsGenplan.CategoryWidth("II, III", 0.10),
                new LufsGenplan.CategoryWidth("IV", 0.00),
                new LufsGenplan.CategoryWidth("М6, М8", 0.20),
                new LufsGenplan.CategoryWidth("А4, А6, А8", 0.10),
                new LufsGenplan.CategoryWidth("Б4, В4, Г4, Е4, Ж4", 0.10),
                new LufsGenplan.CategoryWidth("Б2, В2, Г2", 0.10),
                new LufsGenplan.CategoryWidth("Е2, Ж2", 0.00)
            };
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_2 = new LufsGenplan.RazmType("Разметка_1_2", "Устройство разметки 1.2", "1.2", LufsGenplan.RazmType.typeOfEntity.Line, 
                                                                     paints, cd_1_2, dsl_1_2);
            listOfTypes.Add(type_1_2);

            //RazmType 1.3
            List<LufsGenplan.DashSpace> dsl_1_3 = new List<LufsGenplan.DashSpace>
            {
                new LufsGenplan.DashSpace(1.0d, 0.0d),
                new LufsGenplan.DashSpace(1.0d, 0.0d)
            };
            List<LufsGenplan.CategoryWidth> cd_1_3 = new List<LufsGenplan.CategoryWidth>
            {
                new LufsGenplan.CategoryWidth("I-а", 0.20),
                new LufsGenplan.CategoryWidth("I-б", 0.15),
                new LufsGenplan.CategoryWidth("II, III", 0.10),
                new LufsGenplan.CategoryWidth("IV", 0.00),
                new LufsGenplan.CategoryWidth("М6, М8", 0.20),
                new LufsGenplan.CategoryWidth("А4, А6, А8", 0.10),
                new LufsGenplan.CategoryWidth("Б4, В4, Г4, Е4, Ж4", 0.10),
                new LufsGenplan.CategoryWidth("Б2, В2, Г2", 0.10),
                new LufsGenplan.CategoryWidth("Е2, Ж2", 0.00)
            };
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_3 = new LufsGenplan.RazmType("Разметка_1_3", "Устройство разметки 1.3", "1.3", LufsGenplan.RazmType.typeOfEntity.DoubleLineCenter, 
                                                                     paints, cd_1_3, dsl_1_3);
            listOfTypes.Add(type_1_3);

            //RazmType 1.4
            List<LufsGenplan.DashSpace> dsl_1_4 = new List<LufsGenplan.DashSpace> 
            { 
                new LufsGenplan.DashSpace(1.0d, 0.0d) 
            };
            List<LufsGenplan.CategoryWidth> cd_1_4 = new List<LufsGenplan.CategoryWidth>  
            {
                new LufsGenplan.CategoryWidth("I-а", 0.15),
                new LufsGenplan.CategoryWidth("I-б", 0.10),
                new LufsGenplan.CategoryWidth("II, III", 0.10),
                new LufsGenplan.CategoryWidth("IV", 0.08),
                new LufsGenplan.CategoryWidth("М6, М8", 0.15),
                new LufsGenplan.CategoryWidth("А4, А6, А8", 0.10),
                new LufsGenplan.CategoryWidth("Б4, В4, Г4, Е4, Ж4", 0.10),
                new LufsGenplan.CategoryWidth("Б2, В2, Г2", 0.10),
                new LufsGenplan.CategoryWidth("Е2, Ж2", 0.08)
            };
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("желтая", "ж", 1.0d));
            LufsGenplan.RazmType type_1_4 = new LufsGenplan.RazmType("Разметка_1_4", "Устройство разметки 1.4", "1.4", LufsGenplan.RazmType.typeOfEntity.Line, 
                                                                     paints, cd_1_4, dsl_1_4);
            listOfTypes.Add(type_1_4);

            //RazmType 1.5
            List<LufsGenplan.DashSpace> dsl_1_5 = new List<LufsGenplan.DashSpace> 
            {
                new LufsGenplan.DashSpace(1.0d, 3.0d) 
            };
            List<LufsGenplan.CategoryWidth> cd_1_5 = new List<LufsGenplan.CategoryWidth>  
            {
                new LufsGenplan.CategoryWidth("I-а", 0.15),
                new LufsGenplan.CategoryWidth("I-б", 0.10),
                new LufsGenplan.CategoryWidth("II, III", 0.10),
                new LufsGenplan.CategoryWidth("IV", 0.08),
                new LufsGenplan.CategoryWidth("М6, М8", 0.15),
                new LufsGenplan.CategoryWidth("А4, А6, А8", 0.10),
                new LufsGenplan.CategoryWidth("Б4, В4, Г4, Е4, Ж4", 0.10),
                new LufsGenplan.CategoryWidth("Б2, В2, Г2", 0.10),
                new LufsGenplan.CategoryWidth("Е2, Ж2", 0.08) 
            };
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_5 = new LufsGenplan.RazmType("Разметка_1_5", "Устройство разметки 1.5", "1.5", LufsGenplan.RazmType.typeOfEntity.Line, 
                                                                     paints, cd_1_5, dsl_1_5);
            listOfTypes.Add(type_1_5);

            //RazmType 1.6
            List<LufsGenplan.DashSpace> dsl_1_6 = new List<LufsGenplan.DashSpace> 
            {
                new LufsGenplan.DashSpace(3.0d, 1.0d) 
            };
            List<LufsGenplan.CategoryWidth> cd_1_6 = new List<LufsGenplan.CategoryWidth>  
            {
                new LufsGenplan.CategoryWidth("I-а", 0.15),
                new LufsGenplan.CategoryWidth("I-б", 0.10),
                new LufsGenplan.CategoryWidth("II, III", 0.10),
                new LufsGenplan.CategoryWidth("IV", 0.08),
                new LufsGenplan.CategoryWidth("М6, М8", 0.15),
                new LufsGenplan.CategoryWidth("А4, А6, А8", 0.10),
                new LufsGenplan.CategoryWidth("Б4, В4, Г4, Е4, Ж4", 0.10),
                new LufsGenplan.CategoryWidth("Б2, В2, Г2", 0.10),
                new LufsGenplan.CategoryWidth("Е2, Ж2", 0.08) 
            };
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_6 = new LufsGenplan.RazmType("Разметка_1_6", "Устройство разметки 1.6", "1.6", LufsGenplan.RazmType.typeOfEntity.Line, 
                                                                     paints, cd_1_6, dsl_1_6);
            listOfTypes.Add(type_1_6);

            //RazmType 1.7
            List<LufsGenplan.DashSpace> dsl_1_7 = new List<LufsGenplan.DashSpace> 
            {
                new LufsGenplan.DashSpace(0.5d, 0.5d) 
            };
            List<LufsGenplan.CategoryWidth> cd_1_7 = new List<LufsGenplan.CategoryWidth>  
            {
                new LufsGenplan.CategoryWidth("I-а", 0.15),
                new LufsGenplan.CategoryWidth("I-б", 0.10),
                new LufsGenplan.CategoryWidth("II, III", 0.10),
                new LufsGenplan.CategoryWidth("IV", 0.08),
                new LufsGenplan.CategoryWidth("М6, М8", 0.15),
                new LufsGenplan.CategoryWidth("А4, А6, А8", 0.10),
                new LufsGenplan.CategoryWidth("Б4, В4, Г4, Е4, Ж4", 0.10),
                new LufsGenplan.CategoryWidth("Б2, В2, Г2", 0.10),
                new LufsGenplan.CategoryWidth("Е2, Ж2", 0.08) 
            };
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_7 = new LufsGenplan.RazmType("Разметка_1_7", "Устройство разметки 1.7", "1.7", LufsGenplan.RazmType.typeOfEntity.Line, 
                                                                     paints, cd_1_7, dsl_1_7);
            listOfTypes.Add(type_1_7);

            //RazmType 1.8
            List<LufsGenplan.DashSpace> dsl_1_8 = new List<LufsGenplan.DashSpace> 
            {
                new LufsGenplan.DashSpace(1.0d, 3.0d) 
            };
            List<LufsGenplan.CategoryWidth> cd_1_8 = new List<LufsGenplan.CategoryWidth>  
            {
                new LufsGenplan.CategoryWidth("I-а", 0.20),
                new LufsGenplan.CategoryWidth("I-б", 0.20),
                new LufsGenplan.CategoryWidth("II, III", 0.20),
                new LufsGenplan.CategoryWidth("IV", 0.20),
                new LufsGenplan.CategoryWidth("М6, М8", 0.20),
                new LufsGenplan.CategoryWidth("А4, А6, А8", 0.20),
                new LufsGenplan.CategoryWidth("Б4, В4, Г4, Е4, Ж4", 0.20),
                new LufsGenplan.CategoryWidth("Б2, В2, Г2", 0.20),
                new LufsGenplan.CategoryWidth("Е2, Ж2", 0.20) 
            };
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_8 = new LufsGenplan.RazmType("Разметка_1_8", "Устройство разметки 1.8", "1.8", LufsGenplan.RazmType.typeOfEntity.Line, 
                                                                     paints, cd_1_8, dsl_1_8);
            listOfTypes.Add(type_1_8);

            //RazmType 1.9
            List<LufsGenplan.DashSpace> dsl_1_9 = new List<LufsGenplan.DashSpace> 
            {
                new LufsGenplan.DashSpace(3.0d, 1.0d),
                new LufsGenplan.DashSpace(3.0d, 1.0d)
            };
            List<LufsGenplan.CategoryWidth> cd_1_9 = new List<LufsGenplan.CategoryWidth>  
            {
                new LufsGenplan.CategoryWidth("I-а", 0.10),
                new LufsGenplan.CategoryWidth("I-б", 0.10),
                new LufsGenplan.CategoryWidth("II, III", 0.10),
                new LufsGenplan.CategoryWidth("IV", 0.10),
                new LufsGenplan.CategoryWidth("М6, М8", 0.10),
                new LufsGenplan.CategoryWidth("А4, А6, А8", 0.10),
                new LufsGenplan.CategoryWidth("Б4, В4, Г4, Е4, Ж4", 0.10),
                new LufsGenplan.CategoryWidth("Б2, В2, Г2", 0.10),
                new LufsGenplan.CategoryWidth("Е2, Ж2", 0.10) 
            };
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_9 = new LufsGenplan.RazmType("Разметка_1_9", "Устройство разметки 1.9", "1.9", LufsGenplan.RazmType.typeOfEntity.DoubleLineCenter, 
                                                                     paints, cd_1_9, dsl_1_9);
            listOfTypes.Add(type_1_9);

            //RazmType 1.10
            List<LufsGenplan.DashSpace> dsl_1_10 = new List<LufsGenplan.DashSpace> 
            {
                new LufsGenplan.DashSpace(1.0d, 1.0d)
            };
            List<LufsGenplan.CategoryWidth> cd_1_10 = new List<LufsGenplan.CategoryWidth>  
            {
                new LufsGenplan.CategoryWidth("I-а", 0.15),
                new LufsGenplan.CategoryWidth("I-б", 0.10),
                new LufsGenplan.CategoryWidth("II, III", 0.10),
                new LufsGenplan.CategoryWidth("IV", 0.08),
                new LufsGenplan.CategoryWidth("М6, М8", 0.15),
                new LufsGenplan.CategoryWidth("А4, А6, А8", 0.10),
                new LufsGenplan.CategoryWidth("Б4, В4, Г4, Е4, Ж4", 0.10),
                new LufsGenplan.CategoryWidth("Б2, В2, Г2", 0.10),
                new LufsGenplan.CategoryWidth("Е2, Ж2", 0.08) 
            };
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("желтая", "б", 1.0d));
            LufsGenplan.RazmType type_1_10 = new LufsGenplan.RazmType("Разметка_1_10", "Устройство разметки 1.10", "1.10", LufsGenplan.RazmType.typeOfEntity.Line, 
                                                                      paints, cd_1_10, dsl_1_10);
            listOfTypes.Add(type_1_10);

            //RazmType 1.11
            List<LufsGenplan.DashSpace> dsl_1_11 = new List<LufsGenplan.DashSpace> 
            {
                new LufsGenplan.DashSpace(1.0d, 0.0d),
                new LufsGenplan.DashSpace(3.0d, 1.0d)
            };
            List<LufsGenplan.CategoryWidth> cd_1_11 = new List<LufsGenplan.CategoryWidth>  
            {
                new LufsGenplan.CategoryWidth("I-а", 0.15),
                new LufsGenplan.CategoryWidth("I-б", 0.10),
                new LufsGenplan.CategoryWidth("II, III", 0.10),
                new LufsGenplan.CategoryWidth("IV", 0.08),
                new LufsGenplan.CategoryWidth("М6, М8", 0.15),
                new LufsGenplan.CategoryWidth("А4, А6, А8", 0.10),
                new LufsGenplan.CategoryWidth("Б4, В4, Г4, Е4, Ж4", 0.10),
                new LufsGenplan.CategoryWidth("Б2, В2, Г2", 0.10),
                new LufsGenplan.CategoryWidth("Е2, Ж2", 0.08) 
            };
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_11 = new LufsGenplan.RazmType("Разметка_1_11", "Устройство разметки 1.11", "1.11", LufsGenplan.RazmType.typeOfEntity.DoubleLineSide, 
                                                                      paints, cd_1_11, dsl_1_11);
            listOfTypes.Add(type_1_11);

            //RazmType 1.12
            List<LufsGenplan.DashSpace> dsl_1_12 = new List<LufsGenplan.DashSpace> 
            {
                new LufsGenplan.DashSpace(1.0d, 0.0d)
            };
            List<LufsGenplan.CategoryWidth> cd_1_12 = new List<LufsGenplan.CategoryWidth>  
            {
                new LufsGenplan.CategoryWidth("I-а", 0.40),
                new LufsGenplan.CategoryWidth("I-б", 0.40),
                new LufsGenplan.CategoryWidth("II, III", 0.40),
                new LufsGenplan.CategoryWidth("IV", 0.40),
                new LufsGenplan.CategoryWidth("М6, М8", 0.40),
                new LufsGenplan.CategoryWidth("А4, А6, А8", 0.40),
                new LufsGenplan.CategoryWidth("Б4, В4, Г4, Е4, Ж4", 0.40),
                new LufsGenplan.CategoryWidth("Б2, В2, Г2", 0.40),
                new LufsGenplan.CategoryWidth("Е2, Ж2", 0.40) 
            };
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_12 = new LufsGenplan.RazmType("Разметка_1_12", "Устройство разметки 1.12", "1.12", LufsGenplan.RazmType.typeOfEntity.Line, 
                                                                      paints, cd_1_12, dsl_1_12);
            listOfTypes.Add(type_1_12);

            //RazmType 1.13
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_13 = new LufsGenplan.RazmType("Разметка_1_13", "Устройство разметки 1.13", "1.13", LufsGenplan.RazmType.typeOfEntity.Area, paints);
            listOfTypes.Add(type_1_13);

            //RazmType 1.14.1
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_14_1 = new LufsGenplan.RazmType("Разметка_1_14_1", "Устройство разметки 1.14.1", "1.14.1", LufsGenplan.RazmType.typeOfEntity.Area, paints);
            listOfTypes.Add(type_1_14_1);

            //RazmType 1.14.2
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 0.5d));
            paints.Add(new LufsGenplan.PaintData("желтая", "ж", 0.5d));
            LufsGenplan.RazmType type_1_14_2 = new LufsGenplan.RazmType("Разметка_1_14_2", "Устройство разметки 1.14.2", "1.14.2", LufsGenplan.RazmType.typeOfEntity.Area, paints);
            listOfTypes.Add(type_1_14_2);

            //RazmType 1.14.3
            List<LufsGenplan.DashSpace> dsl_1_14_3 = new List<LufsGenplan.DashSpace> 
            {
                new LufsGenplan.DashSpace(0.6d, 0.4d)
            };
            List<LufsGenplan.CategoryWidth> cd_1_14_3 = new List<LufsGenplan.CategoryWidth>  
            {
                new LufsGenplan.CategoryWidth("I-а", 0.20),
                new LufsGenplan.CategoryWidth("I-б", 0.20),
                new LufsGenplan.CategoryWidth("II, III", 0.20),
                new LufsGenplan.CategoryWidth("IV", 0.20),
                new LufsGenplan.CategoryWidth("М6, М8", 0.20),
                new LufsGenplan.CategoryWidth("А4, А6, А8", 0.20),
                new LufsGenplan.CategoryWidth("Б4, В4, Г4, Е4, Ж4", 0.20),
                new LufsGenplan.CategoryWidth("Б2, В2, Г2", 0.20),
                new LufsGenplan.CategoryWidth("Е2, Ж2", 0.20) 
            };
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_14_3 = new LufsGenplan.RazmType("Разметка_1_14_3", "Устройство разметки 1.14.3", "1.14.3", LufsGenplan.RazmType.typeOfEntity.Line,
                                                                        paints, cd_1_14_3, dsl_1_14_3);
            listOfTypes.Add(type_1_14_3);

            //RazmType 1.15
            List<LufsGenplan.DashSpace> dsl_1_15 = new List<LufsGenplan.DashSpace> 
            {
                new LufsGenplan.DashSpace(0.4d, 0.4d)
            };
            List<LufsGenplan.CategoryWidth> cd_1_15 = new List<LufsGenplan.CategoryWidth>  
            {
                new LufsGenplan.CategoryWidth("I-а", 0.40),
                new LufsGenplan.CategoryWidth("I-б", 0.40),
                new LufsGenplan.CategoryWidth("II, III", 0.40),
                new LufsGenplan.CategoryWidth("IV", 0.40),
                new LufsGenplan.CategoryWidth("М6, М8", 0.40),
                new LufsGenplan.CategoryWidth("А4, А6, А8", 0.40),
                new LufsGenplan.CategoryWidth("Б4, В4, Г4, Е4, Ж4", 0.40),
                new LufsGenplan.CategoryWidth("Б2, В2, Г2", 0.40),
                new LufsGenplan.CategoryWidth("Е2, Ж2", 0.40) 
            };
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_15 = new LufsGenplan.RazmType("Разметка_1_15", "Устройство разметки 1.15", "1.15", LufsGenplan.RazmType.typeOfEntity.Line, 
                                                                      paints, cd_1_15, dsl_1_15);
            listOfTypes.Add(type_1_15);

            //RazmType 1.16.1
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_16_1 = new LufsGenplan.RazmType("Разметка_1_16_1", "Устройство разметки 1.16.1", "1.16.1", LufsGenplan.RazmType.typeOfEntity.Area, paints);
            listOfTypes.Add(type_1_16_1);

            //RazmType 1.16.2
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_16_2 = new LufsGenplan.RazmType("Разметка_1_16_2", "Устройство разметки 1.16.2", "1.16.2", LufsGenplan.RazmType.typeOfEntity.Area, paints);
            listOfTypes.Add(type_1_16_2);

            //RazmType 1.16.3
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_16_3 = new LufsGenplan.RazmType("Разметка_1_16_3", "Устройство разметки 1.16.3", "1.16.3", LufsGenplan.RazmType.typeOfEntity.Area, paints);
            listOfTypes.Add(type_1_16_3);

            //RazmType 1.17.1
            List<LufsGenplan.DashSpace> dsl_1_17_1 = new List<LufsGenplan.DashSpace> 
            {
                new LufsGenplan.DashSpace(1.0d, 0.0d)
            };
            List<LufsGenplan.CategoryWidth> cd_1_17_1 = new List<LufsGenplan.CategoryWidth>  
            {
                new LufsGenplan.CategoryWidth("I-а", 0.10),
                new LufsGenplan.CategoryWidth("I-б", 0.10),
                new LufsGenplan.CategoryWidth("II, III", 0.10),
                new LufsGenplan.CategoryWidth("IV", 0.10),
                new LufsGenplan.CategoryWidth("М6, М8", 0.10),
                new LufsGenplan.CategoryWidth("А4, А6, А8", 0.10),
                new LufsGenplan.CategoryWidth("Б4, В4, Г4, Е4, Ж4", 0.10),
                new LufsGenplan.CategoryWidth("Б2, В2, Г2", 0.10),
                new LufsGenplan.CategoryWidth("Е2, Ж2", 0.10) 
            };
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("желтая", "ж", 1.0d));
            LufsGenplan.RazmType type_1_17_1 = new LufsGenplan.RazmType("Разметка_1_17_1", "Устройство разметки 1.17.1", "1.17.1", LufsGenplan.RazmType.typeOfEntity.Line, 
                                                                        paints, cd_1_17_1, dsl_1_17_1);
            listOfTypes.Add(type_1_17_1);

            //RazmType 1.17.2
            List<LufsGenplan.DashSpace> dsl_1_17_2 = new List<LufsGenplan.DashSpace> 
            {
                new LufsGenplan.DashSpace(1.0d, 0.0d)
            };
            List<LufsGenplan.CategoryWidth> cd_1_17_2 = new List<LufsGenplan.CategoryWidth>  
            {
                new LufsGenplan.CategoryWidth("I-а", 0.10),
                new LufsGenplan.CategoryWidth("I-б", 0.10),
                new LufsGenplan.CategoryWidth("II, III", 0.10),
                new LufsGenplan.CategoryWidth("IV", 0.10),
                new LufsGenplan.CategoryWidth("М6, М8", 0.10),
                new LufsGenplan.CategoryWidth("А4, А6, А8", 0.10),
                new LufsGenplan.CategoryWidth("Б4, В4, Г4, Е4, Ж4", 0.10),
                new LufsGenplan.CategoryWidth("Б2, В2, Г2", 0.10),
                new LufsGenplan.CategoryWidth("Е2, Ж2", 0.10) 
            };
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("желтая", "ж", 1.0d));
            LufsGenplan.RazmType type_1_17_2 = new LufsGenplan.RazmType("Разметка_1_17_2", "Устройство разметки 1.17.2", "1.17.2", LufsGenplan.RazmType.typeOfEntity.Line, 
                                                                        paints, cd_1_17_2, dsl_1_17_2);
            listOfTypes.Add(type_1_17_2);

            //RazmType 1.18
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_18 = new LufsGenplan.RazmType("Разметка_1_18", "Устройство разметки 1.18.1-1.18.8", "1.18.1-1.18.8", LufsGenplan.RazmType.typeOfEntity.Area, paints);
            listOfTypes.Add(type_1_18);

            //RazmType 1.19
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_19 = new LufsGenplan.RazmType("Разметка_1_19", "Устройство разметки 1.19", "1.19", LufsGenplan.RazmType.typeOfEntity.Area, paints);
            listOfTypes.Add(type_1_19);

            //RazmType 1.20
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_20 = new LufsGenplan.RazmType("Разметка_1_20", "Устройство разметки 1.20", "1.20", LufsGenplan.RazmType.typeOfEntity.Area, paints);
            listOfTypes.Add(type_1_20);

            //RazmType 1.21
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_21 = new LufsGenplan.RazmType("Разметка_1_21", "Устройство разметки 1.21", "1.21", LufsGenplan.RazmType.typeOfEntity.Area, paints);
            listOfTypes.Add(type_1_21);

            //RazmType 1.22
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_22 = new LufsGenplan.RazmType("Разметка_1_22", "Устройство разметки 1.22", "1.22", LufsGenplan.RazmType.typeOfEntity.Area, paints);
            listOfTypes.Add(type_1_22);

            //RazmType 1.23
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_23 = new LufsGenplan.RazmType("Разметка_1_23", "Устройство разметки 1.23", "1.23", LufsGenplan.RazmType.typeOfEntity.Area, paints);
            listOfTypes.Add(type_1_23);

            //RazmType 1.24.1
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 5.1715d));
            paints.Add(new LufsGenplan.PaintData("красная", "ж", 1.4308d));
            paints.Add(new LufsGenplan.PaintData("черная", "ч", 0.7818d));
            LufsGenplan.RazmType type_1_24_1 = new LufsGenplan.RazmType("Разметка_1_24_1", "Устройство разметки 1.24.1", "1.24.1", LufsGenplan.RazmType.typeOfEntity.Block, paints);
            listOfTypes.Add(type_1_24_1);

            //RazmType 1.24.2
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 5.1715d));
            paints.Add(new LufsGenplan.PaintData("красная", "к", 1.4308d));
            paints.Add(new LufsGenplan.PaintData("черная", "ч", 0.6874d));
            LufsGenplan.RazmType type_1_24_2 = new LufsGenplan.RazmType("Разметка_1_24_2", "Устройство разметки 1.24.2", "1.24.2", LufsGenplan.RazmType.typeOfEntity.Block, paints);
            listOfTypes.Add(type_1_24_2);

            //RazmType 1.24.3
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 7.6430d));
            paints.Add(new LufsGenplan.PaintData("красная", "к", 1.8601d));
            paints.Add(new LufsGenplan.PaintData("черная", "ч", 0.9500d));
            LufsGenplan.RazmType type_1_24_3 = new LufsGenplan.RazmType("Разметка_1_24_3", "Устройство разметки 1.24.3", "1.24.3", LufsGenplan.RazmType.typeOfEntity.Block, paints);
            listOfTypes.Add(type_1_24_3);

            //RazmType 1.25
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("белая", "б", 1.0d));
            LufsGenplan.RazmType type_1_25 = new LufsGenplan.RazmType("Разметка_1_25", "Устройство разметки 1.25", "1.25", LufsGenplan.RazmType.typeOfEntity.Area, paints);
            listOfTypes.Add(type_1_25);

            //RazmType 1.26
            paints = new List<LufsGenplan.PaintData>();
            paints.Add(new LufsGenplan.PaintData("черная", "ч", 0.3333d));
            paints.Add(new LufsGenplan.PaintData("желтая", "ж", 0.6667d));
            LufsGenplan.RazmType type_1_26 = new LufsGenplan.RazmType("Разметка_1_26", "Устройство разметки 1.26", "1.26", LufsGenplan.RazmType.typeOfEntity.Area, paints);
            listOfTypes.Add(type_1_26);

            
            
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<LufsGenplan.RazmType>));
            StringBuilder sb = new StringBuilder();
            using (System.IO.StringWriter sw = new System.IO.StringWriter(sb))
            {
                serializer.Serialize(sw, listOfTypes);

                if (args.Length == 1)
                {
                    System.IO.File.WriteAllText(args[0] + LufsGenplan.MarkingCalc.ConfigName, sb.ToString());
                }
                else
                {
                    System.IO.File.WriteAllText(@"D:\Work\VyrSl\VyrSl\bin\Release\" + LufsGenplan.MarkingCalc.ConfigName, sb.ToString());
                }
                
            }
        }
    }
}
