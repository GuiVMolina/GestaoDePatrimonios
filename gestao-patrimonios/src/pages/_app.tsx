import { Montserrat } from "next/font/google";
import type { AppProps } from "next/app";
import "@/styles/globals.css";

const montserrat = Montserrat({
  variable: "--font-Montserrat",
  weight: ["400", "500", "600", "700"],
  subsets: ["latin"],
});

export default function App({ Component, pageProps }: AppProps) {
  return (
    <main className={`${montserrat.variable} font-sans`}>
      <Component {...pageProps} />
    </main>
  );
}
