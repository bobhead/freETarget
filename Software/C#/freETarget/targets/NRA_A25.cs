/* NRA A-25 100 yard target */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace freETarget.targets {
    [Serializable]
    class NRA_A25 : aTarget {

        private decimal pelletCaliber;
        private const bool solidInnerTenRing = false;
        private const decimal targetSize = 350; //mm

        private const int trkZoomMin = 0;
        private const int trkZoomMax = 5;
        private const int trkZoomVal = 0;
        private const double zoomStep = 0.32;
        private const decimal pdfZoomFactor = 0.29m;

        private const decimal innerRing = 25.4m; //mm
        private const decimal ring10 = 50.8m; //mm
        private const decimal ring9 = 101.6m; //mm
        private const decimal ring8 = 152.4m; //mm
        private const decimal ring7 = 203.2m; //mm
        private const decimal ring6 = 254m; //mm
        private const decimal outterRing = 304.8m; //mm

        private const int blackRings = 7;  // Largest Black Ring
        private const decimal blackCircle = 203.2m; //mm
        private const int firstRing = 5;  // Largest Ring

        private decimal innerTenRadius;
        private decimal r10, r9, r8, r7, r6, r5;

        private static readonly decimal[] rings = new decimal[] { outterRing, ring6, ring7, ring8, ring9, ring10, innerRing };


        public NRA_A25(decimal caliber) : base(caliber) {
            this.pelletCaliber = caliber;
            innerTenRadius = innerRing / 2m + pelletCaliber / 2m;
            r10 = ring10 / 2m + pelletCaliber / 2m;
            r9 = ring9 / 2m + pelletCaliber / 2m;
            r8 = ring8 / 2m + pelletCaliber / 2m;
            r7 = ring7 / 2m + pelletCaliber / 2m;
            r6 = ring6 / 2m + pelletCaliber / 2m;
            r5 = outterRing / 2m + pelletCaliber / 2m;
        }

        public override decimal getScore(decimal radius) {
            if (radius <= 0) {
                return 10.9m;
            } else if (radius <= r10) {
                return 10.9m - (radius / r10) * 0.9m;
            } else if (radius <= r9) {
                return 10m - ((radius - r10) / (r9 - r10));
            } else if (radius <= r8) {
                return 9m - ((radius - r9) / (r8 - r9));
            } else if (radius <= r7) {
                return 8m - ((radius - r8) / (r7 - r8));
            } else if (radius <= r6) {
                return 7m - ((radius - r7) / (r6 - r7));
            } else if (radius <= r5) {
                return 6m - ((radius - r6) / (r5 - r6));
            } else {
                return 0;
            }
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
            return typeof(NRA_A25).FullName;
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
