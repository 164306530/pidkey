            XmlDocument doc = new XmlDocument();
            doc.Load(pkey);
            MemoryStream stream = new MemoryStream(Convert.FromBase64String(doc.GetElementsByTagName("tm:infoBin")[0].InnerText));
            doc.Load(stream);
            XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
            ns.AddNamespace("pkc", "http://www.microsoft.com/DRM/PKEY/Configuration/2.0");

            XmlNodeList xNode = doc.SelectNodes("/pkc:ProductKeyConfiguration/pkc:PublicKeys/pkc:PublicKey", ns);
            try
            {
                XmlNode node1 = doc.SelectSingleNode("/pkc:ProductKeyConfiguration/pkc:Configurations/pkc:Configuration[pkc:ActConfigId='" + aid + "']", ns);
                if (node1 != null)
                {
                    ActConfigId= node1.ChildNodes[0].InnerText;
                    groupid = node1.ChildNodes[1].InnerText;
                    EditionId= node1.ChildNodes[2].InnerText;
                    ProductDescription = node1.ChildNodes[3].InnerText;
                    ProductKeyType = node1.ChildNodes[4].InnerText;
                }
                else
                {
                    node1 = doc.SelectSingleNode("/pkc:ProductKeyConfiguration/pkc:Configurations/pkc:Configuration[pkc:ActConfigId='" + aid.ToUpper() + "']", ns);
                    ActConfigId= node1.ChildNodes[0].InnerText;
                    groupid = node1.ChildNodes[1].InnerText;
                    EditionId= node1.ChildNodes[2].InnerText;
                    ProductDescription = node1.ChildNodes[3].InnerText;
                    ProductKeyType = node1.ChildNodes[4].InnerText;
                }

                XmlNode node = doc.SelectSingleNode("/pkc:ProductKeyConfiguration/pkc:KeyRanges/pkc:KeyRange[pkc:RefActConfigId='" + ActConfigId + "']", ns);
                if (node != null)
                {
                    PartNumber = node.ChildNodes[1].InnerText;
                    EulaType = node.ChildNodes[2].InnerText;
                }
                XmlNode node = doc.SelectSingleNode("/pkc:ProductKeyConfiguration/pkc:PublicKeys/pkc:PublicKey[pkc:GroupId='" + groupid + "']", ns);
                if (node != null)
                {
                    AlgorithmId = node.ChildNodes[1].InnerText;
                    PublicKeyValue = node.ChildNodes[2].InnerText;
                }

            }
