/* NRA A-50 50 meter ISSF target */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace freETarget.targets {
    [Serializable]
    class NRA_A50 : aTarget {

        private decimal pelletCaliber;
        private const bool solidInnerTenRing = false;
        private const decimal targetSize = 220; //mm

        private const int trkZoomMin = 0;
        private const int trkZoomMax = 5;
        private const int trkZoomVal = 0;
        private const double zoomStep = 0.57;
        private const decimal pdfZoomFactor = 0.29m;

        private const decimal innerRing = 5m; //mm
        private const decimal ring10 = 10.4m; //mm
        private const decimal ring9 = 26.4m; //mm
        private const decimal ring8 = 42.4m; //mm
        private const decimal ring7 = 58.4m; //mm
        private const decimal ring6 = 74.4m; //mm
        private const decimal ring5 = 90.4m; //mm
        private const decimal ring4 = 106.4m; //mm
        private const decimal ring3 = 122.4m; //mm
        private const decimal ring2 = 138.4m; //mm
        private const decimal outterRing = 154.4m; //mm

        private const int blackRings = 4;  // Largest Black Ring
        private const decimal blackCircle = 112.4m; //mm
        private const int firstRing = 1;  // Largest Ring

        private decimal innerTenRadius;

        private static readonly decimal[] rings = new decimal[] { outterRing, ring2, ring3, ring4, ring5, ring6, ring7, ring8, ring9, ring10, innerRing };


        public NRA_A50(decimal caliber) : base(caliber) {
            this.pelletCaliber = caliber;
            innerTenRadius = innerRing / 2m + pelletCaliber / 2m;
        }

        public override int getBlackRings() {
            return blackRings;
        }

        public override decimal getInnerTenRadius() {
            return innerTenRadius;
        }

        public override decimal getOutterRadius() {
            return getOutterRing() / 2m + pelletCaliber / 2m;
        }

        public override decimal get10Radius() {
            return ring10 / 2m + pelletCaliber / 2m;
        }

        public override string getName() {
            return typeof(NRA_A50).FullName;
        }

        public override decimal getOutterRing() {
            return outterRing;
        }

        public override decimal getProjectileCaliber() {
            return pelletCaliber;
        }

        public override decimal[] getRings() {
            return rings;
        }

        public override decimal getSize() {
            return targetSize;
        }

        public override decimal getZoomFactor(int value) {
            return (decimal)(1 / Math.Pow(2, value * zoomStep));
        }

        public override bool isSolidInner() {
            return solidInnerTenRing;
        }

        public override int getTrkZoomMaximum() {
            return trkZoomMax;
        }

        public override int getTrkZoomMinimum() {
            return trkZoomMin;
        }

        public override int getTrkZoomValue() {
            return trkZoomVal;
        }

        public override float getFontSize(float diff) {
            return 10;
        }

        public override decimal getBlackDiameter() {
            return blackCircle;
        }

        public override int getRingTextCutoff() {
            return 9;
        }

        public override float getTextOffset(float diff, int ring) {
            return 0;
        }

        public override decimal getPDFZoomFactor(List<Shot> shotList) {
            if (shotList == null) {
                return pdfZoomFactor;
            } else {
                bool zoomed = true;
                foreach (Shot s in shotList) {
                    if (s.score < 6) {
                        zoomed = false;
                    }
                }

                if (zoomed) {
                    return 0.5m;
                } else {
                    return 1;
                }
            }
        }


        public override int getTextRotation() {
            return 0;
        }

        public override int getFirstRing() {
            return firstRing;
        }

        public override (decimal, decimal) rapidFireBarDimensions() {
            return (-1, -1);
        }

        public override bool drawNorthText() {
            return false;
        }

        public override bool drawSouthText() {
            return false;
        }

        public override bool drawWestText() {
            return true;
        }

        public override bool drawEastText() {
            return false;
        }
    }
}
