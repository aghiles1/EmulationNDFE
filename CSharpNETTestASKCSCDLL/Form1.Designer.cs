namespace CSharpNETTestASKCSCDLL
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.Button1 = new System.Windows.Forms.Button();
            this.txtCard = new System.Windows.Forms.TextBox();
            this.txtCom = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.data = new System.Windows.Forms.TextBox();
            this.write = new System.Windows.Forms.Button();
            this.binary = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.text = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.URI = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.code = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Button1
            // 
            this.Button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button1.Location = new System.Drawing.Point(12, 15);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(140, 31);
            this.Button1.TabIndex = 1;
            this.Button1.Text = "Detect Card";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // txtCard
            // 
            this.txtCard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCard.Location = new System.Drawing.Point(264, 154);
            this.txtCard.Name = "txtCard";
            this.txtCard.ReadOnly = true;
            this.txtCard.Size = new System.Drawing.Size(155, 13);
            this.txtCard.TabIndex = 6;
            // 
            // txtCom
            // 
            this.txtCom.Location = new System.Drawing.Point(325, 118);
            this.txtCom.Name = "txtCom";
            this.txtCom.ReadOnly = true;
            this.txtCom.Size = new System.Drawing.Size(88, 20);
            this.txtCom.TabIndex = 5;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(261, 121);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(34, 13);
            this.Label1.TabIndex = 4;
            this.Label1.Text = "COM:";
            // 
            // data
            // 
            this.data.Location = new System.Drawing.Point(194, 12);
            this.data.Multiline = true;
            this.data.Name = "data";
            this.data.Size = new System.Drawing.Size(346, 89);
            this.data.TabIndex = 7;
            // 
            // write
            // 
            this.write.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.write.Location = new System.Drawing.Point(12, 374);
            this.write.Name = "write";
            this.write.Size = new System.Drawing.Size(75, 31);
            this.write.TabIndex = 8;
            this.write.Text = "write";
            this.write.UseVisualStyleBackColor = true;
            this.write.Click += new System.EventHandler(this.button2_Click);
            // 
            // binary
            // 
            this.binary.Location = new System.Drawing.Point(106, 298);
            this.binary.Name = "binary";
            this.binary.Size = new System.Drawing.Size(481, 20);
            this.binary.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 294);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 24);
            this.label2.TabIndex = 10;
            this.label2.Text = "Binary :";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // text
            // 
            this.text.Location = new System.Drawing.Point(106, 208);
            this.text.Name = "text";
            this.text.Size = new System.Drawing.Size(481, 20);
            this.text.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 24);
            this.label3.TabIndex = 12;
            this.label3.Text = "Text :";
            // 
            // URI
            // 
            this.URI.Location = new System.Drawing.Point(106, 254);
            this.URI.Name = "URI";
            this.URI.Size = new System.Drawing.Size(481, 20);
            this.URI.TabIndex = 14;
            this.URI.TextChanged += new System.EventHandler(this.URI_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 250);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 24);
            this.label4.TabIndex = 15;
            this.label4.Text = "URL :";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // code
            // 
            this.code.Location = new System.Drawing.Point(147, 347);
            this.code.Multiline = true;
            this.code.Name = "code";
            this.code.Size = new System.Drawing.Size(373, 94);
            this.code.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 450);
            this.Controls.Add(this.code);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.URI);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.text);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.binary);
            this.Controls.Add(this.write);
            this.Controls.Add(this.data);
            this.Controls.Add(this.txtCard);
            this.Controls.Add(this.txtCom);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Button1);
            this.Name = "Form1";
            this.Text = "SmartCard_Project";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.TextBox txtCard;
        internal System.Windows.Forms.TextBox txtCom;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.TextBox data;
        private System.Windows.Forms.Button write;
        private System.Windows.Forms.TextBox binary;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox text;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox URI;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox code;
    }
}

