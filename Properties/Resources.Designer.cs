﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Il codice è stato generato da uno strumento.
//     Versione runtime:4.0.30319.42000
//
//     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
//     il codice viene rigenerato.
// </auto-generated>
//------------------------------------------------------------------------------

namespace gameboy_printer_windows.Properties {
    using System;
    
    
    /// <summary>
    ///   Classe di risorse fortemente tipizzata per la ricerca di stringhe localizzate e così via.
    /// </summary>
    // Questa classe è stata generata automaticamente dalla classe StronglyTypedResourceBuilder.
    // tramite uno strumento quale ResGen o Visual Studio.
    // Per aggiungere o rimuovere un membro, modificare il file con estensione ResX ed eseguire nuovamente ResGen
    // con l'opzione /str oppure ricompilare il progetto VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Restituisce l'istanza di ResourceManager nella cache utilizzata da questa classe.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("gameboy_printer_windows.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Esegue l'override della proprietà CurrentUICulture del thread corrente per tutte le
        ///   ricerche di risorse eseguite utilizzando questa classe di risorse fortemente tipizzata.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Cerca una stringa localizzata simile a # GAMEBOY PRINTER EMULATION PROJECT
        ///# By Brian Khuu (2017)
        ///!{&quot;command&quot;:&quot;INIT&quot;}
        ///!{&quot;command&quot;:&quot;DATA&quot;,&quot;compressed&quot;:0,&quot;more&quot;:1}
        ///FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF
        ///FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF
        ///FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF
        ///FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF
        ///FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF
        ///FF FF F8 FF E0 F0 C1 E0 80 C3 80 83 00 83 80 03
        ///FF FF 7F FF 5F 3F 8F 1F 4F BF 3F 1F 0A 14 44 30
        ///FF FF FF FF FF FF FF FF FF FF 3E FF 3C 18  [stringa troncata]&quot;;.
        /// </summary>
        internal static string Test {
            get {
                return ResourceManager.GetString("Test", resourceCulture);
            }
        }
    }
}
