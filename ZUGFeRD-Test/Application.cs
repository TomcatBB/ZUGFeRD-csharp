﻿/*
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 * 
 *   http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using s2industries.ZUGFeRD;

namespace ZUGFeRD_Test
{
    class Application
    {
        internal void run()
        {
            _loadSampleZUGFeRDInvoice();
            _saveSampleZUGFeRDInvoice();
        } // !run()


        private void _loadSampleZUGFeRDInvoice()
        {
            string path = @"..\..\..\demodata\ZUGFeRD-invoice-1.xml";
            InvoiceDescriptor desc = InvoiceDescriptor.Load(path);

            Assert.AreEqual(desc.Profile, Profile.Comfort);
            Assert.AreEqual(desc.Type, InvoiceType.Invoice);
        } // !_loadSampleZUGFeRDInvoice()


        private void _saveSampleZUGFeRDInvoice()
        {
            InvoiceDescriptor desc = InvoiceDescriptor.CreateInvoice("471102", new DateTime(2013, 6, 5), CurrencyCodes.EUR, "GE2020211-471102");
            desc.Profile = Profile.Comfort;
            desc.ReferenceOrderNo = "AB-312";
            desc.AddNote("Rechnung gemäß Bestellung Nr. 2013-471331 vom 01.03.2013.");
            desc.AddNote("Es bestehen Rabatt- und Bonusvereinbarungen.", SubjectCode.AAA);
            desc.SetBuyer("Kunden Mitte AG", "69876", "Frankfurt", "Kundenstraße", "15", "DE", "0088", "4000001987658");
            desc.AddBuyerTaxRegistration("DE234567890", TaxRegistrationSchemeID.VA);
            desc.SetBuyerContact("Hans Muster");
            desc.SetSeller("Lieferant GmbH", "80333", "München", "Lieferantenstraße", "20", "DE", "0088", "4000001123452");
            desc.AddSellerTaxRegistration("201/113/40209", TaxRegistrationSchemeID.FC);
            desc.AddSellerTaxRegistration("DE123456789", TaxRegistrationSchemeID.VA);
            desc.SetBuyerOrderReferenceDocument("2013-471331", new DateTime(2013, 03, 01));
            desc.SetDeliveryNoteReferenceDocument("2013-51111", new DateTime(2013, 6, 3));
            desc.ActualDeliveryDate = new DateTime(2013, 6, 3);
            desc.SetTotals(202.76m, 5.80m, 14.73m, 193.83m, 21.31m, 215.14m, 50.0m, 165.14m);
            desc.AddApplicableTradeTax(9.06m, 129.37m, 7m, TaxTypes.VAT, TaxCategoryCodes.S);
            desc.AddApplicableTradeTax(12.25m, 64.46m, 19m, TaxTypes.VAT, TaxCategoryCodes.S);
            desc.AddLogisticsServiceCharge(5.80m, "Versandkosten", TaxTypes.VAT, TaxCategoryCodes.S, 7m);
            desc.AddTradeAllowanceCharge(true, 10m, CurrencyCodes.EUR, 1m, "Sondernachlass", TaxTypes.VAT, TaxCategoryCodes.S, 19);
            desc.AddTradeAllowanceCharge(true, 137.7m, CurrencyCodes.EUR, 13.73m, "Sondernachlass", TaxTypes.VAT, TaxCategoryCodes.S, 7);
            desc.SetTradePaymentTerms("Zahlbar innerhalb 30 Tagen netto bis 04.07.2013, 3% Skonto innerhalb 10 Tagen bis 15.06.2013", new DateTime(2013, 07, 04));

            desc.addTradeLineItem("0160", "4012345001235",
                                  "KR3M", "55T01",
                                  "Kunstrasen grün 3m breit",
                                  "300cm x 100 cm",
                                  QuantityCodes.MTK, 1,
                                  4.00m,
                                  3.3333m,
                                  3,
                                  TaxTypes.VAT, TaxCategoryCodes.S, 19);

            desc.addTradeLineItem("0160", "4000050986428",
                                  "SFK5", "55T02",
                                  "Schweinesteak",
                                  "aus Deutschland",
                                  QuantityCodes.KGM, 1,
                                  5.50m,
                                  5.50m,
                                  5,
                                  TaxTypes.VAT, TaxCategoryCodes.S, 7);


            desc.addTradeLineItem("0160", "4000001234561", 
                                  "GTRWA5", "55T03", 
                                  "Mineralwasser Medium 12 x 1,0l PET",
                                  "",
                                  QuantityCodes.C62, 1,
                                  5.49m,
                                  5.49m,
                                  20,
                                  TaxTypes.VAT, TaxCategoryCodes.S, 7);

            desc.Save("output.xml");
        } // !_saveSampleZUGFeRDInvoice()
    }
}