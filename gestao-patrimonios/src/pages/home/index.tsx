import Button from "@/components/button/button";
import Header from "@/components/header/header";
import Lista from "@/components/lista/lista";
import Link from "next/link";

const Home = () => {
  return (
    <>
      <main>
        <Header />
        <section className="min_height">
          <div className="container column gap">
            <div className="row full_width">
              <h1>Locais</h1>
              <div className="side">
                <input
                  type="text"
                  placeholder="Pesquise o ambiente..."
                  className="input"
                />
                <Link href="/local/novo" className="btn full_width">
                  + Local
                </Link>
                <Button className="btn2">=</Button>
              </div>
            </div>
            <Lista pages="home" />
          </div>
        </section>
      </main>
    </>
  );
};

export default Home;
